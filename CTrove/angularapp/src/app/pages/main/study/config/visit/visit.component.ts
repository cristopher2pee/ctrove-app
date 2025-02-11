import { AfterViewInit, Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { NzModalRef, NzModalService } from 'ng-zorro-antd/modal';
import { ADD, EDIT, SCHEDULED, UNSCHEDULED, VISIT_PAGE } from 'src/Utilities/common/app-strings';
import { VISIT_FIELDS, Visit, VisitFilter } from 'src/app/models/dto/visit';
import { BaseMasterListComponent } from 'src/app/pages/common/base-class';
import { VisitService } from 'src/app/services/main/config/visit.service';
import { ManageVisitComponent } from './manage-visit/manage-visit.component';
import { DEF_MODAL_WIDTH } from 'src/Utilities/common/app-variables';

@Component({
  selector: 'app-visit',
  templateUrl: './visit.component.html',
  styleUrls: ['./visit.component.css']
})
export class VisitComponent  extends BaseMasterListComponent implements AfterViewInit {
  fields = VISIT_FIELDS
  override headers = [this.fields.code, this.fields.name, this.fields.targetDays.name, this.fields.timeWindow.name, this.fields.visitType]

  form = new FormGroup({
    search: new FormControl(),
    status: new FormControl(true)
  })

  constructor(private service: VisitService,
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
    this.service.getList(page, limit, new VisitFilter().deserialize(this.form.getRawValue())).subscribe({
      next: (response) => {
        this.meta = response.meta
        this.data = response.data.map(d => ({
          id: d.id,
          code: d.code,
          name: d.name,
          targetDays: d.targetDays > 1 ? `${d.targetDays} days` : `${d.targetDays} day`,
          timeWindow: d.timeWindow > 1 ? `${d.timeWindow} days` : `${d.timeWindow} day`,
          visitType: d.visitType === 1 ? SCHEDULED : UNSCHEDULED
        }))

        if(this.meta.page <= 0)
          this.meta.page = 1
      },
      error: () => this.isLoading = false,
      complete: () => this.isLoading = false
    })

  tableChanged = (val: any) => this.getList(val.pageIndex, val.pageSize)

  manage(isAdd: boolean, data: Visit | null = null) {
    const modal: NzModalRef = this.modalService.create({
      nzTitle: `${ isAdd ? ADD : EDIT } ${VISIT_PAGE}`,
      nzContent: ManageVisitComponent,
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
