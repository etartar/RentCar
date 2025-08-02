import { ChangeDetectionStrategy, Component, ElementRef, inject, signal, viewChild, ViewEncapsulation } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { FormValidateDirective } from 'form-validate-angular';
import { HttpService } from '../../../services/http-service';
import { FlexiToastService } from 'flexi-toast';

@Component({
  imports: [
    FormsModule,
    FormValidateDirective
  ],
  templateUrl: './login.html',
  encapsulation: ViewEncapsulation.None,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export default class Login {
  readonly loading = signal<boolean>(false);
  readonly email = signal<string>("");
  readonly emailOrUserName = signal<string>("");
  readonly tfaCode = signal<string>("");
  readonly tfaConfirmCode = signal<string>("");
  readonly showTFAForm = signal<boolean>(false);
  readonly tfaTime = signal<{min: number, sec: number}>({min:5, sec: 0});

  readonly closeBtn = viewChild<ElementRef<HTMLButtonElement>>("modalCloseBtn");

  readonly http = inject(HttpService);
  readonly router = inject(Router);
  readonly toast = inject(FlexiToastService);
  
  readonly passwordEl = viewChild<ElementRef<HTMLInputElement>>("passwordEl");

  togglePassword(){
    this.passwordEl()?.nativeElement.type === "password" 
      ? this.passwordEl()?.nativeElement.setAttribute("type", "text")
      : this.passwordEl()?.nativeElement.setAttribute("type", "password");
  }


  login(form: NgForm) {
    if (!form.valid) return;

    this.loading.set(true);

    this.http.post<{token: string | null, tfaCode: string | null}>("/rent/auth/login", form.value, (res) => {
      if (res.token !== null){
        localStorage.setItem("response", res.token);
        this.router.navigateByUrl("/");
      } else if (res.tfaCode !== null){
        this.showTFAForm.set(true);
        this.tfaCode.set(res.tfaCode);
        this.tfaTime.set({min: 5, sec: 0});

        var interval:any = setInterval(() => {
          let min = this.tfaTime().min;
          let sec = this.tfaTime().sec;

          sec--;

          if (sec < 0){
            sec = 59;
            min --;

            if (min < 0){
              min = 0;
              interval.clear();
              this.showTFAForm.set(false);
            }
          }

          this.tfaTime.set({min: min, sec: sec});
        }, 1000);
      }

      this.loading.set(false);
    }, () => {
      this.loading.set(false);
    });
  }

  loginWithTFA(form: NgForm) {
    if (!form.valid) return;

    const data = {
      emailOrUserName: this.emailOrUserName(),
      tfaCode: this.tfaCode(),
      tfaConfirmCode: this.tfaConfirmCode()
    }

    this.loading.set(true);

    this.http.post<{token: string | null, tfaCode: string | null}>("/rent/auth/login-with-tfa", data, (res) => {
      localStorage.setItem("response", res.token!);
      this.router.navigateByUrl("/");
      this.loading.set(false);
    }, () => {
      this.loading.set(false);
    });
  }

  forgotPassword(){
    this.http.post<string>(`/rent/auth/forgot-password/${this.email()}`, {}, (res) => {
      this.toast.showToast("Başarılı", res, "info");
      this.closeBtn()!.nativeElement.click();
    });
  }
}
