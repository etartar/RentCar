import { httpResource } from '@angular/common/http';
import { ChangeDetectionStrategy, Component, computed, effect, inject, signal, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import Blank from 'apps/admin/src/components/blank/blank';
import { initialProtectionPackageModel, ProtectionPackageModel } from 'apps/admin/src/models/protection-package.model';
import { Result } from 'apps/admin/src/models/result.model';
import { BreadcrumbModel, BreadcrumbService } from 'apps/admin/src/services/breadcrumb';
import { TrCurrencyPipe } from 'tr-currency';

@Component({
  imports: [
    Blank,
    TrCurrencyPipe
  ],
  templateUrl: './detail.html',
  encapsulation: ViewEncapsulation.None,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export default class Detail {
  readonly id = signal<string>('');
  readonly breadcrumbs = signal<BreadcrumbModel[]>([]);
  readonly pageTitle = computed(() => this.data()?.name ?? 'Güvence Paketi  Detay');

  readonly result = httpResource<Result<ProtectionPackageModel>>(() => `/rent/protection-packages/${this.id()}`);
  readonly data = computed(() => this.result.value()?.data ?? initialProtectionPackageModel);
  readonly loading = computed(() => this.result.isLoading());

  readonly #breadcrum = inject(BreadcrumbService);
  readonly #activated = inject(ActivatedRoute);

  constructor(){
    this.#activated.params.subscribe(res => {
      this.id.set(res['id']);
    });

    effect(() => {
      const breadcrumbs: BreadcrumbModel[] = [
        {
          title: 'Güvence Paketleri',
          icon: 'bi-shield-check',
          url: '/protection-packages'
        }
      ];

      if (this.data()){
        this.breadcrumbs.set(breadcrumbs);
        this.breadcrumbs.update(prev => [...prev, {
          title: this.data().name,
          icon: 'bi-zoom-in',
          url: `/protection-packages/detail/${this.id()}`,
          isActive: true
        }]);

        this.#breadcrum.reset(this.breadcrumbs());
      }
    });
  }
}
