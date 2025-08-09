import { httpResource } from '@angular/common/http';
import { ChangeDetectionStrategy, Component, computed, effect, inject, signal, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import Blank from 'apps/admin/src/components/blank/blank';
import { CategoryModel, initialCategory } from 'apps/admin/src/models/category.model';
import { Result } from 'apps/admin/src/models/result.model';
import { BreadcrumbModel, BreadcrumbService } from 'apps/admin/src/services/breadcrumb';

@Component({
  imports: [
    Blank,
  ],
  templateUrl: './detail.html',
  encapsulation: ViewEncapsulation.None,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export default class Detail {
  readonly id = signal<string>('');
  readonly breadcrumbs = signal<BreadcrumbModel[]>([]);
  readonly pageTitle = computed(() => this.data()?.name ?? 'Kategori Detay');

  readonly result = httpResource<Result<CategoryModel>>(() => `/rent/categories/${this.id()}`);
  readonly data = computed(() => this.result.value()?.data ?? initialCategory);
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
          title: 'Kategoriler',
          icon: 'bi-tags',
          url: '/categories'
        }
      ];

      if (this.data()){
        this.breadcrumbs.set(breadcrumbs);
        this.breadcrumbs.update(prev => [...prev, {
          title: this.data().name,
          icon: 'bi-zoom-in',
          url: `/categories/detail/${this.id()}`,
          isActive: true
        }]);

        this.#breadcrum.reset(this.breadcrumbs());
      }
    });
  }
}
