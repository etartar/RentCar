import { NgClass } from '@angular/common';
import { ChangeDetectionStrategy, Component, computed, inject, linkedSignal, resource, signal, ViewEncapsulation } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import Blank from 'apps/admin/src/components/blank/blank';
import { BranchModel, initialBranch } from 'apps/admin/src/models/branch.model';
import { BreadcrumbModel, BreadcrumbService } from 'apps/admin/src/services/breadcrumb';
import { HttpService } from 'apps/admin/src/services/http-service';
import { FlexiToastService } from 'flexi-toast';
import { FormValidateDirective } from 'form-validate-angular';
import { NgxMaskDirective } from 'ngx-mask';
import { lastValueFrom } from 'rxjs';

@Component({
  imports: [
    Blank,
    FormsModule,
    FormValidateDirective,
    NgClass,
    NgxMaskDirective
  ],
  templateUrl: './create.html',
  encapsulation: ViewEncapsulation.None,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export default class Create {
  readonly id = signal<string | undefined>(undefined);
  readonly breadcrumbs = signal<BreadcrumbModel[]>([
    {
      title: 'Şubeler',
      icon: 'bi-buildings',
      url: '/branches'
    }
  ]);

  readonly pageTitle = computed(() => this.id() ? 'Şube Güncelle' : 'Şube Ekle');
  readonly pageIcon = computed(() => this.id() ? 'bi-pen' : 'bi-plus');
  readonly buttonName = computed(() => this.id() ? 'Güncelle' : 'Kaydet');
  readonly result = resource({
    params: () => this.id(),
    loader: async () => {
      var res = await lastValueFrom(this.#http.getResource<BranchModel>(`/rent/branches/${this.id()}`));

      this.breadcrumbs.update(prev => [...prev, {
        title: res.data!.name,
        icon: 'bi-pen',
        url: `/branches/edit/${this.id()}`,
        isActive: true
      }]);

      this.#breadcrum.reset(this.breadcrumbs());

      return res.data;
    }
  });
  readonly data = linkedSignal(() => this.result.value() ?? {...initialBranch});
  readonly loading = linkedSignal(() => this.result.isLoading());

  readonly #breadcrum = inject(BreadcrumbService);
  readonly #activated = inject(ActivatedRoute);
  readonly #http = inject(HttpService);
  readonly #toast = inject(FlexiToastService);
  readonly #router = inject(Router);

  constructor(){
    this.#activated.params.subscribe(res => {
      if (res['id']){
        this.id.set(res['id']);
      } else {
        this.breadcrumbs.update(prev => [...prev, {
          title: 'Ekle',
          icon: 'bi-plus',
          url: '/branches/add',
          isActive: true
        }]);

        this.#breadcrum.reset(this.breadcrumbs());
      }
    });
  }

  save(form: NgForm){
    if (!form.valid) return;

    this.loading.set(true);

    if (!this.id()){
      this.#http.post<string>('/rent/branches', this.data(), (res) => {
        this.#toast.showToast("Başarılı", res, "success");
        this.#router.navigateByUrl("/branches");
        this.loading.set(false);
      }, () => this.loading.set(false));
    } else {
      this.#http.put<string>('/rent/branches', this.data(), (res) => {
        this.#toast.showToast("Başarılı", res, "info");
        this.#router.navigateByUrl("/branches");
        this.loading.set(false);
      }, () => this.loading.set(false));
    }
  }

  changeStatus(status: boolean){
    this.data.update(prev => ({
      ...prev,
      isActive: status
    }));
  }
}
