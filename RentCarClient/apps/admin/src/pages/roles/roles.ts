import { ChangeDetectionStrategy, Component, signal, ViewEncapsulation } from '@angular/core';
import Grid from '../../components/grid/grid';
import { FlexiGridModule } from 'flexi-grid';
import { BreadcrumbModel } from '../../services/breadcrumb';
import { RouterLink } from '@angular/router';

@Component({
  imports: [
    Grid,
    FlexiGridModule,
    RouterLink
  ],
  templateUrl: './roles.html',
  encapsulation: ViewEncapsulation.None,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export default class Roles {
  readonly breadcrumbs = signal<BreadcrumbModel[]>([
    {
      title: 'Roller',
      icon: 'bi-buildings',
      url: '/roles',
      isActive: true
    }
  ]);
}
