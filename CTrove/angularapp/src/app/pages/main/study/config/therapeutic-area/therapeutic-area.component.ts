import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { NzModalRef, NzModalService } from 'ng-zorro-antd/modal';
import { ADD, EDIT, THERAPEUTIC_AREA_PAGE } from 'src/Utilities/common/app-strings';
import { TAListFilter, THERAPEUTIC_AREA_FIELDS, TherapeuticArea } from 'src/app/models/dto/therapeutic-area';
import { BaseMasterListComponent } from 'src/app/pages/common/base-class';
import { DEF_MODAL_WIDTH } from 'src/Utilities/common/app-variables';
import { ManageTherapeuticAreaComponent } from './manage-therapeutic-area/manage-therapeutic-area.component';
import { TherapeuticAreaService } from 'src/app/services/main/config/therapeutic-area.service';

@Component({
  selector: 'app-therapeutic-area',
  templateUrl: './therapeutic-area.component.html',
  styleUrls: ['./therapeutic-area.component.css']
})
export class TherapeuticAreaComponent extends BaseMasterListComponent implements OnInit {
  @Output() dataLoaded = new EventEmitter
  fields = THERAPEUTIC_AREA_FIELDS
  override headers = [this.fields.code, this.fields.name]

  form = new FormGroup({
    search: new FormControl(),
    status: new FormControl(true)
  })

  constructor(private service: TherapeuticAreaService,
    private modalService: NzModalService) {
    super();
  }

  ngOnInit() {
    this.getList()
    this.form.valueChanges.subscribe(_ => this.getList(this.meta.page, this.meta.limit))
  }

  open(isOpen: boolean) {
    if(isOpen)
      this.getList()
  }

  getList = (page: number = this.meta.page, limit: number = this.meta.limit) => 
    this.service.getList(page, limit, new TAListFilter().deserialize(this.form.getRawValue())).subscribe({
      next: (response) => {
        this.meta = response.meta
        this.data = response.data.map(d => ({
          id: d.id,
          code: d.code,
          name: d.name
        }))

        if(this.meta.page <= 0)
          this.meta.page = 1

        this.dataLoaded.emit(response.data)
      },
      error: () => this.isLoading = false,
      complete: () => this.isLoading = false
    })

  tableChanged = (val: any) => this.getList(val.pageIndex, val.pageSize)

  manage(isAdd: boolean, data: TherapeuticArea | null = null) {
    const modal: NzModalRef = this.modalService.create({
      nzTitle: `${ isAdd ? ADD : EDIT } ${THERAPEUTIC_AREA_PAGE}`,
      nzContent: ManageTherapeuticAreaComponent,
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
