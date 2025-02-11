import { AfterViewInit, Component, EventEmitter, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { NzModalRef, NzModalService } from 'ng-zorro-antd/modal';
import { ADD, EDIT, TRIAL_CLASSIFICATION_PAGE } from 'src/Utilities/common/app-strings';
import { TCFilter, TRIAL_CLASSIFICATION_FIELD, TrialClassification } from 'src/app/models/dto/trial-classification';
import { BaseMasterListComponent } from 'src/app/pages/common/base-class';
import { ManageTrialClassificationComponent } from './manage-trial-classification/manage-trial-classification.component';
import { DEF_MODAL_WIDTH } from 'src/Utilities/common/app-variables';
import { TrialClassificationService } from 'src/app/services/main/config/trial-classification.service';

@Component({
  selector: 'app-trial-classification',
  templateUrl: './trial-classification.component.html',
  styleUrls: ['./trial-classification.component.css']
})
export class TrialClassificationComponent extends BaseMasterListComponent implements AfterViewInit {
  @Output() dataLoaded = new EventEmitter
  fields = TRIAL_CLASSIFICATION_FIELD
  override headers = [this.fields.code, this.fields.name]

  form = new FormGroup({
    search: new FormControl(),
    status: new FormControl(true)
  })

  constructor(private service: TrialClassificationService,
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
    this.service.getList(page, limit, new TCFilter().deserialize(this.form.getRawValue())).subscribe({
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

  manage(isAdd: boolean, data: TrialClassification | null = null) {
    const modal: NzModalRef = this.modalService.create({
      nzTitle: `${ isAdd ? ADD : EDIT } ${TRIAL_CLASSIFICATION_PAGE}`,
      nzContent: ManageTrialClassificationComponent,
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
