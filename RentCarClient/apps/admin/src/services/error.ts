import { HttpErrorResponse } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { FlexiToastService } from 'flexi-toast';

@Injectable({
  providedIn: 'root'
})
export class ErrorService {
  readonly #toast = inject(FlexiToastService);
  readonly #router = inject(Router);

  handle(err: HttpErrorResponse){
    console.log(err);

    const status = err.status;

    if (status === 400 || status === 403 || status === 422 || status === 500){
      const messages = err.error.errorMessages;

      messages.forEach((val: string) => {
        this.#toast.showToast("Hata!", val, "error");
      });
    } else if (status === 401){
      const messages = "Tekrar giriş yapmalısınız.";

      localStorage.clear();
      this.#toast.showToast("Hata!", messages, "error");
      this.#router.navigateByUrl("/login");
    } else {
      this.#toast.showToast("Hata!", "Bir hata oluştu. Lütfen yöneticinize danışın", "error");
    }
  }
}
