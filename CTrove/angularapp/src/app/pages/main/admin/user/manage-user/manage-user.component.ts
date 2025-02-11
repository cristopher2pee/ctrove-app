import { AfterViewInit, Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Guid } from 'guid-typescript';
import { NzModalRef, NzModalService } from 'ng-zorro-antd/modal';
import { ACCESS_PAGE, ADD, ADD_INVITE_USER_SUCCESS, EDIT, INACTIVATE_ERROR, INVALID_INPUTS, NEW_RECORD, SOMETHING_WENT_WRONG, UPDATE_USER_SUCCESS } from 'src/Utilities/common/app-strings';
import { ACCESS_FIELDS, Access } from 'src/app/models/dto/access';
import { Role } from 'src/app/models/dto/role';
import { USER_FIELDS, User } from 'src/app/models/dto/user';
import { BaseModalComponent } from 'src/app/pages/common/base-class';
import { CommonService } from 'src/app/services/common/common.service';
import { UserService } from 'src/app/services/main/user.service';
import { ManageAccessComponent } from '../../access/manage-access/manage-access.component';
import { DEF_MODAL_WIDTH } from 'src/Utilities/common/app-variables';
import { LocationService } from 'src/app/services/common/location.service';
import { DEFAULT_SIDE_TABLE_META } from 'src/app/models/dto/common';

@Component({
  selector: 'app-manage-user',
  templateUrl: './manage-user.component.html',
  styleUrls: ['./manage-user.component.css']
})
export class ManageUserComponent extends BaseModalComponent implements OnInit, AfterViewInit {
  @Input() override isAdd: boolean = true
  @Input() rolesOption!: Role[]
  override meta = DEFAULT_SIDE_TABLE_META
  override headers = [ACCESS_FIELDS.accessLevel, ACCESS_FIELDS.rights, ACCESS_FIELDS.status]
  override data!: Access[]
  user: User | null = null
  fields = USER_FIELDS
  accessData!: any[]

  form = new FormGroup({
    id: new FormControl(Guid.create().toString()),
    firstname: new FormControl('', [Validators.required]),
    lastname: new FormControl('', [Validators.required]),
    middlename: new FormControl(),
    suffix: new FormControl(),
    prefix: new FormControl(),
    email: new FormControl('', [Validators.required, Validators.email]),
    mobile: new FormControl('', [Validators.required]),
    landline: new FormControl(),
    organization: new FormControl('', [Validators.required]),
    status: new FormControl(true),
    date: new FormControl(this.commonService.setDateRange()),
    startDate: new FormControl(new Date, [Validators.required]),
    endDate: new FormControl(new Date, [Validators.required]),
    rolesId: new FormControl('', [Validators.required]),
    remarks: new FormControl(NEW_RECORD),
  })

  constructor(private modal: NzModalRef,
    private userService: UserService,
    private commonService: CommonService,
    private modalService: NzModalService,
    private locationService: LocationService) {
    super()
  }

  ngOnInit() {
    if(!this.isAdd && this.user) {
      this.form.controls.remarks.setValue(null)
      this.form.controls.remarks.addValidators([Validators.required])
      this.userService.getById(this.user.id)
        .subscribe({
          next: (response) => {
            if(!response)
              this.commonService.showErrorMessage()
            
            this.form.patchValue(this.user = response)  
            this.form.controls.date.setValue(this.commonService.setDateRange(response.startDate, response.endDate))
          },
          error: () => {
            this.commonService.showErrorMessage()
            this.isLoading = false
          },
          complete: () => {
            this.isLoading = false
            this.getList()
          }
        })
    }  
  }

  ngAfterViewInit() {
    this.form.controls.date.valueChanges.subscribe(response => {
      if(!response)
        return
      this.form.patchValue({
        startDate: response[0],
        endDate: response[1]
      })
    })
  }

  async onSubmit(isSave: boolean) {
    if(isSave) {
      if(this.form.valid) {
        this.isLoading = true
        var d = new User().deserialize(this.form.getRawValue())
        d.location = await this.locationService.getSavedAddress()
        if(this.isAdd) 
          this.userService.save(d).subscribe({
            next: (userResponse) => {
              if (!userResponse)
                this.commonService.showErrorMessage()

              this.userService.inviteEmail(userResponse).subscribe({
                next: (response) => {
                  if(!response)
                    this.commonService.showErrorMessage()
                  
                  this.commonService.showMessage(ADD_INVITE_USER_SUCCESS)
                  this.modal.destroy(true)
                },
                error: () => {
                  this.commonService.showErrorMessage()
                  this.isLoading = false
                },
                complete: () => this.isLoading = false
              })
            },
            error: () => {
              this.commonService.showErrorMessage()
              this.isLoading = false
            },
            complete: () => this.isLoading = false
          })
        else {
          this.userService.update(d).subscribe({
            next: (response) => {
              if(response)
                this.commonService.showMessage(UPDATE_USER_SUCCESS)
            },
            error: () => {
              this.commonService.showErrorMessage()
              this.isLoading = false
            },
            complete: () => {
              this.isLoading = false
              if(!d.status && d.status != this.user?.status) 
                this.userService.remove(d).subscribe({
                  error: () => this.commonService.showErrorMessage(INACTIVATE_ERROR)
                })
              this.modal.destroy(true)
            }
          })
        }
      }
      else {
        this.form.markAllAsTouched()
        this.commonService.showErrorMessage(INVALID_INPUTS)
      }
    }
    else
      this.modal.destroy()
  }

  manage(isAdd: boolean, access: Access | null = null) {
    const modal: NzModalRef = this.modalService.create({
      nzTitle: `${ isAdd ? ADD : EDIT } ${ACCESS_PAGE}`,
      nzContent: ManageAccessComponent,
      nzWidth: DEF_MODAL_WIDTH,
      nzComponentParams: {
        isAdd: isAdd,
        access: access,
        userId: isAdd ? this.form.controls.id.value : null
      },
      nzFooter: null
    })

    modal.afterClose.subscribe(response => {
      if(response)
        this.getList()
    })
  }

  getList = () => {
    if(!this.form.value.id)
      return

    this.userService.getAccessList(this.form.value.id).subscribe({
      next: (response) => {
        this.accessData = response.map(d => ({
          id: d.accessResponse.id,
          accessLevel: d.sitesResponse ? d.sitesResponse.name : d.studyCountry ? d.studyCountry.name : null,
          rights: this.commonService.formatRights(d.accessResponse.rights),
          status: this.commonService.formatStatus(d.accessResponse.status)
        }))
      },
      error: () => {
        this.commonService.showErrorMessage(SOMETHING_WENT_WRONG)
        this.isSideLoading = false
      },
      complete: () => this.isSideLoading = false
    })
  }

  onBlur(req: number, val: any) {
    switch(req) {
      case 1: {
        if(!val || (!this.isAdd && val === this.user?.email))
          return
        this.userService.validateEmail(val).subscribe({
          next: (response) => {
            if(response)
              this.form.controls.email.setErrors({ 'email-exist' : true })
          },
          error: () => this.commonService.showErrorMessage()
        })
      }
      break;
    }
  }
}
