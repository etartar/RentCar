import { httpResource } from '@angular/common/http';
import { AfterViewInit, ChangeDetectionStrategy, Component, computed, contentChild, contentChildren, inject, input, linkedSignal, signal, TemplateRef, ViewEncapsulation } from '@angular/core';
import { FlexiGridColumnComponent, FlexiGridModule, FlexiGridService, StateModel } from 'flexi-grid';
import { ODataModel } from '../../models/odata.model';
import { RouterLink } from '@angular/router';
import { FlexiToastService } from 'flexi-toast';
import { HttpService } from '../../services/http-service';
import { BreadcrumbModel, BreadcrumbService } from '../../services/breadcrumb';
import { NgTemplateOutlet } from '@angular/common';
import { Common } from '../../services/common';

export interface btnOptions{
  url: string;
  permission: string;
}

@Component({
  selector: 'app-grid',
  imports: [
    FlexiGridModule,
    RouterLink,
    NgTemplateOutlet
  ],
  templateUrl: './grid.html',
  encapsulation: ViewEncapsulation.None,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export default class Grid implements AfterViewInit {
  readonly pageTitle = input.required<string>();
  readonly captionTitle = input.required<string>();
  readonly endpoint = input.required<string>();
  readonly showAudit = input<boolean>(true);
  readonly addOptions = input.required<btnOptions>()
  readonly editOptions = input.required<btnOptions>();
  readonly detailOptions = input.required<btnOptions>();
  readonly deleteOptions = input.required<btnOptions>();
  readonly breadcrumbs = input.required<BreadcrumbModel[]>();
  readonly commandColumnWidth = input<string>("150px");
  readonly showIndex = input<boolean>(false);

  readonly columns = contentChildren(FlexiGridColumnComponent, {descendants: true});
  readonly commandTemplateRef = contentChild<TemplateRef<any>>("commandTemplate");
  readonly columnCommandTemplateRef = contentChild<TemplateRef<any>>("columnCommandTemplate");

  readonly state = signal<StateModel>(new StateModel());
  readonly result = httpResource<ODataModel<any>>(() => {
    let endpoint = this.endpoint() + '?$count=true';
    const part = this.#grid.getODataEndpoint(this.state());
    endpoint += `&${part}`;

    return endpoint;
  });
  readonly data = computed(() => this.result.value()?.value ?? []);
  readonly total = computed(() => this.result.value()?.['@odata.count'] ?? 0);
  readonly loading = linkedSignal(() => this.result.isLoading());

  readonly #grid = inject(FlexiGridService);
  readonly #http = inject(HttpService);
  readonly #toast = inject(FlexiToastService);
  readonly #breadcrum = inject(BreadcrumbService);  
  readonly #common = inject(Common);

  ngAfterViewInit(): void {
    this.#breadcrum.reset(this.breadcrumbs());
  }

  dataStateChange(state: StateModel){
    this.state.set(state);
  }

  delete(id: string){
    this.#toast.showSwal("Sil?", "Bu kaydı silmek istiyor musunuz?", "Sil", () => {
      this.loading.set(true);
      this.#http.delete<string>(`${this.deleteOptions().url}/${id}`, res => {
        this.#toast.showToast("Başarılı", res, "info");
        this.result.reload();
        this.loading.set(false);
      }, () => this.loading.set(false));
    });
  }

  checkPermission(permission: string){
    return this.#common.checkPermission(permission);
  }
}
