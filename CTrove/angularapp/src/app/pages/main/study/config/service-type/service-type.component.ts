import { AfterViewInit, Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { NzModalRef, NzModalService } from 'ng-zorro-antd/modal';
import { ADD, EDIT, SERVICE_TYPE_PAGE } from 'src/Utilities/common/app-strings';
import { SERVICE_TYPE_FIELDS, ServiceType, ServiceTypeFilter } from 'src/app/models/dto/service-type';
import { BaseMasterListComponent } from 'src/app/pages/common/base-class';
import { ManageServiceTypeComponent } from './manage-service-type/manage-service-type.component';
import { DEF_MODAL_WIDTH } from 'src/Utilities/common/app-variables';
import { ServiceTypeService } from 'src/app/services/main/config/service-type.service';

@Component({
  selector: 'app-service-type',
  templateUrl: './service-type.component.html',
  styleUrls: ['./service-type.component.css']
})
export class ServiceTypeComponent extends BaseMasterListComponent implements AfterViewInit {
  fields = SERVICE_TYPE_FIELDS
  override headers = [this.fields.code, this.fields.name]

  form = new FormGroup({
    search: new FormControl(),
    status: new FormControl(true)
  })

  constructor(private service: ServiceTypeService,
    private modalService: NzModalService) {
    super();
  }

  ngAfterViewInit() {
    this.getList()
    this.form.valueChanges.subscribe(_ => this.getList(this.meta.page, this.meta.limit))
  }

  open(isOpen: boolean) {
    if(isOpen)
      this.getList()
  }

  getList = (page: number = this.meta.page, limit: number = this.meta.limit) => 
    this.service.getList(page, limit, new ServiceTypeFilter().deserialize(this.form.getRawValue())).subscribe({
      next: (response) => {
        this.meta = response.meta
        this.data = response.data.map(d => ({
          id: d.id,
          code: d.code,
          name: d.name
        }))

        if(this.meta.page <= 0)
          this.meta.page = 1
      },
      error: () => this.isLoading = false,
      complete: () => this.isLoading = false
    })

  tableChanged = (val: any) => this.getList(val.pageIndex, val.pageSize)

  manage(isAdd: boolean, data: ServiceType | null = null) {
    const modal: NzModalRef = this.modalService.create({
      nzTitle: `${ isAdd ? ADD : EDIT } ${SERVICE_TYPE_PAGE}`,
      nzContent: ManageServiceTypeComponent,
      nzWidth: DEF_MODAL_WIDTH,
      nzComponentParams: {
        isAdd: isAdd,
        data: data,
      },
      nzFooter: null
    })

    modal.afterClose.subscribe(response => {
      if(!response)
        return

      this.reset()
      this.getList()
    })
  }
}
