import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Guid } from 'guid-typescript';
import { NzModalRef } from 'ng-zorro-antd/modal';
import { PAGE_OPTIONS } from 'src/Utilities/common/app-data';
import { ADD_ROLE_SUCCESS, INACTIVATE_ERROR, INVALID_INPUTS, NEW_RECORD, NO_SELECTED_PAGE, SOMETHING_WENT_WRONG, UPDATE_ROLE_SUCCESS } from 'src/Utilities/common/app-strings';
import { ROLES_FIELDS, Role } from 'src/app/models/dto/role';
import { BaseModalComponent } from 'src/app/pages/common/base-class';
import { CommonService } from 'src/app/services/common/common.service';
import { LocationService } from 'src/app/services/common/location.service';
import { RolesService } from 'src/app/services/main/roles.service';

@Component({
  selector: 'app-manage-roles',
  templateUrl: './manage-roles.component.html',
  styleUrls: ['./manage-roles.component.css']
})
export class ManageRolesComponent extends BaseModalComponent implements OnInit {
  fields = ROLES_FIELDS
  pages = PAGE_OPTIONS.map(d => {
    d.status = false
    return d
  })  
  role!: Role | null

  form = new FormGroup({
    id: new FormControl(Guid.create().toString()),
    code: new FormControl('',[Validators.required]),
    name: new FormControl('', [Validators.required]),
    blinded: new FormControl(false, [Validators.required]),
    status: new FormControl(true),
    remarks: new FormControl(NEW_RECORD),
  })

  constructor(private modal: NzModalRef,
    private commonService: CommonService,
    private roleService: RolesService,
    private locationService: LocationService) {
    super();
  }
  
  ngOnInit() {     
    if(!this.isAdd && this.role) {
      this.form.controls.remarks.setValue(null)
      this.form.controls.remarks.addValidators([Validators.required])
      this.isLoading = true
      this.roleService.getById(this.role.id)
        .subscribe({
          next: (response) => {
            if(!response)
              this.commonService.showErrorMessage()

            this.form.patchValue(this.role = response)  

            this.pages = this.pages.map(d => {
              let p = response.rolesPages.find(p => p.pages === d.pages)

              if(p) {
                d.id = p.id
                d.rolesId = p.rolesId
                d.status = p.status
              }
                
              return d
            })

          },
          error: () => this.commonService.showErrorMessage(SOMETHING_WENT_WRONG),
          complete: () => this.isLoading = false
        })
    }
  }

  async onSubmit(isSave: boolean) {
    if(isSave) {
      if(this.form.valid) {
        if(!this.pages.some(d => d.status)) {
          this.commonService.showErrorMessage(NO_SELECTED_PAGE)
          return
        }

        this.isLoading = true
        var d = new Role().deserialize(this.form.getRawValue())
        d.rolesPages = this.pages
        d.location = await this.locationService.getSavedAddress()
        if(this.isAdd) 
          this.roleService.save(d)
          .subscribe({
            next: (response) => {
              if(response)
                this.commonService.showMessage(ADD_ROLE_SUCCESS)
            },
            error: () => {
              this.commonService.showErrorMessage(SOMETHING_WENT_WRONG)
              this.isLoading = false
            },
            complete: () => {
              this.isLoading = false
              this.modal.destroy(true)
            },
          })
        else {
          this.roleService.update(d)
            .subscribe({
              next: (response) => {
                if(response)
                  this.commonService.showMessage(UPDATE_ROLE_SUCCESS)
              },
              error: () => {
                this.commonService.showErrorMessage(SOMETHING_WENT_WRONG)
                this.isLoading = false
              },
              complete: () => {
                this.isLoading = false
                if(!d.status && d.status != this.role?.status) 
                  this.roleService.remove(d).subscribe({
                    error: () => this.commonService.showErrorMessage(INACTIVATE_ERROR)
                  })
                this.modal.destroy(true)
              },
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
}
