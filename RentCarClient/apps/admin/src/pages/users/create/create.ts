import { NgClass } from '@angular/common';
import { httpResource } from '@angular/common/http';
import { ChangeDetectionStrategy, Component, computed, inject, linkedSignal, resource, signal, ViewEncapsulation } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import Blank from 'apps/admin/src/components/blank/blank';
import { BranchModel } from 'apps/admin/src/models/branch.model';
import { ODataModel } from 'apps/admin/src/models/odata.model';
import { RoleModel } from 'apps/admin/src/models/role.model';
import { initialUser, UserModel } from 'apps/admin/src/models/user.model';
import { BreadcrumbModel, BreadcrumbService } from 'apps/admin/src/services/breadcrumb';
import { Common } from 'apps/admin/src/services/common';
import { HttpService } from 'apps/admin/src/services/http-service';
import { FlexiSelectModule } from 'flexi-select';
import { FlexiToastService } from 'flexi-toast';
import { FormValidateDirective } from 'form-validate-angular';
import { lastValueFrom } from 'rxjs';

@Component({
  imports: [
    Blank,
    FormsModule,
    FormValidateDirective,
    NgClass,
    FlexiSelectModule
  ],
  templateUrl: './create.html',
  encapsulation: ViewEncapsulation.None,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export default class Create {
  readonly id = signal<string | undefined>(undefined);
  readonly breadcrumbs = signal<BreadcrumbModel[]>([
    {
      title: 'Kullanıcılar',
      icon: 'bi-people',
      url: '/users'
    }
  ]);

  readonly pageTitle = computed(() => this.id() ? 'Kullanıcı Güncelle' : 'Kullanıcı Ekle');
  readonly pageIcon = computed(() => this.id() ? 'bi-pen' : 'bi-plus');
  readonly buttonName = computed(() => this.id() ? 'Güncelle' : 'Kaydet');
  readonly result = resource({
    params: () => this.id(),
    loader: async () => {
      var res = await lastValueFrom(this.#http.getResource<UserModel>(`/rent/users/${this.id()}`));

      this.breadcrumbs.update(prev => [...prev, {
        title: res.data!.fullName,
        icon: 'bi-pen',
        url: `/users/edit/${this.id()}`,
        isActive: true
      }]);

      this.#breadcrum.reset(this.breadcrumbs());

      return res.data;
    }
  });
  readonly data = linkedSignal(() => this.result.value() ?? {...initialUser});
  readonly loading = linkedSignal(() => this.result.isLoading());

  readonly branchResult = httpResource<ODataModel<BranchModel>>(() => '/rent/odata/branches');
  readonly branches = computed(() => this.branchResult.value()?.value ?? []);
  readonly branchLoading = computed(() => this.branchResult.isLoading());
  
  readonly roleResult = httpResource<ODataModel<RoleModel>>(() => '/rent/odata/roles');
  readonly roles = computed(() => this.roleResult.value()?.value ?? []);
  readonly roleLoading = computed(() => this.roleResult.isLoading());

  readonly #breadcrum = inject(BreadcrumbService);
  readonly #activated = inject(ActivatedRoute);
  readonly #http = inject(HttpService);
  readonly #toast = inject(FlexiToastService);
  readonly #router = inject(Router);
  readonly #common = inject(Common);

  constructor(){
    this.#activated.params.subscribe(res => {
      if (res['id']){
        this.id.set(res['id']);
      } else {
        this.breadcrumbs.update(prev => [...prev, {
          title: 'Ekle',
          icon: 'bi-plus',
          url: '/users/add',
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
      this.#http.post<string>('/rent/users', this.data(), (res) => {
        this.#toast.showToast("Başarılı", res, "success");
        this.#router.navigateByUrl("/users");
        this.loading.set(false);
      }, () => this.loading.set(false));
    } else {
      this.#http.put<string>('/rent/users', this.data(), (res) => {
        this.#toast.showToast("Başarılı", res, "info");
        this.#router.navigateByUrl("/users");
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

  checkIsAdmin(){
    return this.#common.decode().role === "SysAdmin";
  }
}
