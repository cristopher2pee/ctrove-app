import { AfterViewInit, Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { NzModalRef, NzModalService } from 'ng-zorro-antd/modal';
import { ACCESS_PAGE, ADD, EDIT } from 'src/Utilities/common/app-strings';
import { ACCESS_FIELDS, Access, AccessListFilter } from 'src/app/models/dto/access';
import { BaseMasterListComponent } from 'src/app/pages/common/base-class';
import { CommonService } from 'src/app/services/common/common.service';
import { AccessService } from 'src/app/services/main/access.service';
import { ManageAccessComponent } from './manage-access/manage-access.component';
import { DEF_MODAL_WIDTH } from 'src/Utilities/common/app-variables';
import { RIGHTS_OPTIONS } from 'src/Utilities/common/app-data';
import { MessagingService } from 'src/app/services/common/messaging.service';

@Component({
  selector: 'app-access',
  templateUrl: './access.component.html',
  styleUrls: ['./access.component.css']
})
export class AccessComponent extends BaseMasterListComponent implements AfterViewInit {
  fields = ACCESS_FIELDS
  override headers = [this.fields.accessLevel, this.fields.user, this.fields.rights, this.fields.status]
  rightsOption = RIGHTS_OPTIONS

  form = new FormGroup ({
    search: new FormControl(),
    status: new FormControl(true),
    rightsId: new FormControl([1]),
    right: new FormControl(1)
  })

  constructor(private accessService: AccessService,
    private commonService: CommonService,
    private modalService: NzModalService,
    private messagingService: MessagingService) {
    super();
  }

  ngAfterViewInit() {
    this.getList(this.meta.page, this.meta.limit)

    this.form.get('search')?.valueChanges
      .subscribe(_ => this.getList(this.meta.page, this.meta.limit))

    this.form.get('status')?.valueChanges
      .subscribe(_ => this.getList(this.meta.page, this.meta.limit))

    this.form.get('rightsId')?.valueChanges
      .subscribe(_ => {
        this.form.controls.right.setValue((_ as number[]).reduce((num1, num2) => num1 + num2, 0))
        this.getList(this.meta.page, this.meta.limit)
      })
  }

  getList = (page: number = this.meta.page, limit: number = this.meta.limit) => this.accessService.getList(page, limit, new AccessListFilter().deserialize(this.form.getRawValue())).subscribe({
    next: (response) => {
      this.meta = response.meta
      this.data = response.data.map(d => ({
        id: d.accessResponse.id,
        accessLevel: d.sitesResponse ? d.sitesResponse.name : d.studyCountry ? d.studyCountry.name : null,
        user: this.commonService.formatName(d.accessResponse.user),
        rights: this.commonService.formatRights(d.accessResponse.rights),
        status: this.commonService.formatStatus(d.accessResponse.status)
      }))

      if(this.meta.page <= 0)
        this.meta.page = 1
    },
    error: () => this.isLoading = false,
    complete: () => this.isLoading = false
  })
  
  tableChanged = (val: any) => this.getList(val.pageIndex, val.pageSize)

  manage(isAdd: boolean, access: Access | null = null) {
    const modal: NzModalRef = this.modalService.create({
      nzTitle: `${ isAdd ? ADD : EDIT } ${ACCESS_PAGE}`,
      nzContent: ManageAccessComponent,
      nzWidth: DEF_MODAL_WIDTH,
      nzComponentParams: {
        isAdd: isAdd,
        access: access
      },
      nzFooter: null
    })

    modal.afterClose.subscribe(response => {
      if(!response)
        return

      this.reset()
      this.getList()
      this.messagingService.userSiteOnChange()
    })
  }
}
