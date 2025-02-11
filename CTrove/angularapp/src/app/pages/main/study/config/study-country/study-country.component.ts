import { AfterViewInit, Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { NzModalRef, NzModalService } from 'ng-zorro-antd/modal';
import { ADD, EDIT, STUDY_COUNTRY_PAGE } from 'src/Utilities/common/app-strings';
import { STUDY_COUNTRY_FIELDS, StudyCountry, StudyCountryFilter } from 'src/app/models/dto/study-country';
import { BaseMasterListComponent } from 'src/app/pages/common/base-class';
import { ManageStudyCountryComponent } from './manage-study-country/manage-study-country.component';
import { DEF_MODAL_WIDTH } from 'src/Utilities/common/app-variables';
import { StudyCountryService } from 'src/app/services/main/config/study-country.service';

@Component({
  selector: 'app-study-country',
  templateUrl: './study-country.component.html',
  styleUrls: ['./study-country.component.css']
})
export class StudyCountryComponent extends BaseMasterListComponent implements AfterViewInit {
  fields = STUDY_COUNTRY_FIELDS
  override headers = [this.fields.code, this.fields.name]

  form = new FormGroup({
    search: new FormControl(),
    status: new FormControl(true)
  })

  constructor(private service: StudyCountryService,
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
    this.service.getList(page, limit, new StudyCountryFilter().deserialize(this.form.getRawValue())).subscribe({
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

  manage(isAdd: boolean, data: StudyCountry | null = null) {
    const modal: NzModalRef = this.modalService.create({
      nzTitle: `${ isAdd ? ADD : EDIT } ${STUDY_COUNTRY_PAGE}`,
      nzContent: ManageStudyCountryComponent,
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
