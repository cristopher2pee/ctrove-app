import { AfterViewInit, Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { NzModalRef, NzModalService } from 'ng-zorro-antd/modal';
import { ManageUserComponent } from 'src/app/pages/main/admin/user/manage-user/manage-user.component';
import { Role } from 'src/app/models/dto/role';
import { USER_FIELDS, User, UserListFilter } from 'src/app/models/dto/user';
import { BaseMasterListComponent } from 'src/app/pages/common/base-class';
import { CommonService } from 'src/app/services/common/common.service';
import { ResourceService } from 'src/app/services/common/resource.service';
import { UserService } from 'src/app/services/main/user.service';
import { ADD, EDIT, USER_PAGE } from 'src/Utilities/common/app-strings';
import { LARGE_MODAL_WIDTH } from 'src/Utilities/common/app-variables';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent extends BaseMasterListComponent implements AfterViewInit {
  fields = USER_FIELDS
  override headers = [this.fields.prefix.name, this.fields.lastName.name, this.fields.firstName.name, this.fields.suffix.name, this.fields.email, this.fields.mobileNumber.name, this.fields.landline.name, this.fields.startDate, this.fields.endDate, this.fields.organization.name, this.fields.status]
  rolesOption!: Role[]

  form = new FormGroup ({
    search: new FormControl(),
    status: new FormControl(true),
    rolesId: new FormControl()
  })

  constructor(private userService: UserService,
    private commonService: CommonService,
    private resourceService: ResourceService,
    private modalService: NzModalService) {
    super()
  }

  ngAfterViewInit(): void {
    this.getList()

    this.form.valueChanges.subscribe(_ => this.getList(this.meta.page, this.meta.limit))

    this.resourceService.getInviteResource()
    .subscribe({
      next: (response) => {
        this.rolesOption = response as Role[]
      }
    })
  }

  getList = (page: number = this.meta.page, limit: number = this.meta.limit) => 
    this.userService.getList(page, limit, new UserListFilter().deserialize(this.form.getRawValue())).subscribe({
      next: (response) => {
        this.meta = response.meta
        this.data = response.data.map(d => ({
          id: d.id,
          prefix: d.prefix,
          lastname: d.lastname,
          firstname: d.firstname,
          suffix: d.suffix,
          email: d.email,
          mobile: d.mobile,
          landling: d.landline,
          startDate: this.commonService.formatDate(d.startDate),
          endDate: this.commonService.formatDate(d.endDate),
          organization: d.organization,
          status: this.commonService.formatStatus(d.status)
        }))

        if(this.meta.page <= 0)
          this.meta.page = 1
      },
      error: () => this.isLoading = false,
      complete: () => this.isLoading = false
    })

  tableChanged = (val: any) => this.getList(val.pageIndex, val.pageSize)

  manage(isAdd: boolean, user: User | null = null) {
    const modal: NzModalRef = this.modalService.create({
      nzTitle: `${ isAdd ? ADD : EDIT } ${USER_PAGE}`,
      nzContent: ManageUserComponent,
      nzWidth: LARGE_MODAL_WIDTH,
      nzComponentParams: {
        isAdd: isAdd,
        user: user,
        rolesOption: this.rolesOption
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
