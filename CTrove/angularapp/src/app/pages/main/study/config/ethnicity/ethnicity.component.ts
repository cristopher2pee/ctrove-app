import { AfterViewInit, Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { NzModalRef, NzModalService } from 'ng-zorro-antd/modal';
import { ADD, EDIT, ETHNICITY_PAGE } from 'src/Utilities/common/app-strings';
import { DEF_MODAL_WIDTH } from 'src/Utilities/common/app-variables';
import { ETHNICITY_FIELDS, Ethnicity, EthnicityListFilter } from 'src/app/models/dto/ethnicity';
import { BaseMasterListComponent } from 'src/app/pages/common/base-class';
import { EthnicityService } from 'src/app/services/main/config/ethnicity.service';
import { ManageEthnicityComponent } from './manage-ethnicity/manage-ethnicity.component';
import { combineLatestWith } from 'rxjs';

@Component({
  selector: 'app-ethnicity',
  templateUrl: './ethnicity.component.html',
  styleUrls: ['./ethnicity.component.css']
})
export class EthnicityComponent extends BaseMasterListComponent implements AfterViewInit, OnInit {
  fields = ETHNICITY_FIELDS
  override headers = [this.fields.code, this.fields.name]

  form = new FormGroup({
    search: new FormControl(),
    status: new FormControl(true),
  })

  constructor(private service: EthnicityService,
    private modal: NzModalService) {
    super();
  }

  ngOnInit() {
  }

  ngAfterViewInit() {
    this.getList()
    this.form.controls.search.valueChanges
      .pipe(combineLatestWith(this.form.controls.status.valueChanges))
      .subscribe(_ => this.getList(this.meta.page, this.meta.limit))
  }

  getList = (page: number = this.meta.page, limit: number = this.meta.limit) => 
    this.service.getList(page, limit, new EthnicityListFilter().deserialize(this.form.getRawValue())).subscribe({
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

  manage(isAdd: boolean, d: Ethnicity | null = null) {
    const modal: NzModalRef = this.modal.create({
      nzTitle: `${ isAdd ? ADD : EDIT } ${ETHNICITY_PAGE}`,
      nzContent: ManageEthnicityComponent,
      nzWidth: DEF_MODAL_WIDTH,
      nzComponentParams: {
        isAdd: isAdd,
        data: d,
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
