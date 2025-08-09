import { ChangeDetectionStrategy, Component, inject, signal, ViewEncapsulation } from '@angular/core';
import Grid from '../../components/grid/grid';
import { FlexiGridModule } from 'flexi-grid';
import { BreadcrumbModel } from '../../services/breadcrumb';
import { Common } from '../../services/common';

@Component({
  imports: [
    Grid,
    FlexiGridModule
  ],
  templateUrl: './extras.html',
  encapsulation: ViewEncapsulation.None,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export default class Extras {
  readonly breadcrumbs = signal<BreadcrumbModel[]>([
    {
      title: 'Ekstralar',
      icon: 'bi-plus-square',
      url: '/extras',
      isActive: true
    }
  ]);
  
  readonly #common = inject(Common);

  checkPermission(permission: string){
    return this.#common.checkPermission(permission);
  }
}
