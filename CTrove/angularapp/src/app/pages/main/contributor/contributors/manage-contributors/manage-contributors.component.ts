import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Guid } from 'guid-typescript';
import { NzModalRef } from 'ng-zorro-antd/modal';
import { GRADE_TYPE_OPTIONS, PREFIX_TYPE_OPTIONS } from 'src/Utilities/common/app-data';
import { ADD_CONTRIBUTOR_SUCCESS, EMAIL_EXIST, INACTIVATE_ERROR, INVALID_INPUTS, NAME_EXIST, NEW_RECORD, UPDATE_CONTRIBUTOR_SUCCESS } from 'src/Utilities/common/app-strings';
import { Contributor, FIELDS } from 'src/app/models/contributor/contributor';
import { FIELDS as STUDIES_FIELDS } from 'src/app/models/contributor/contributor-study';
import { Country } from 'src/app/models/contributor/country';
import { BaseModalComponent } from 'src/app/pages/common/base-class';
import { CommonService } from 'src/app/services/common/common.service';
import { LocationService } from 'src/app/services/common/location.service';
import { ContributorService } from 'src/app/services/main/contributor/contributor.service';

@Component({
  selector: 'app-manage-contributors',
  templateUrl: './manage-contributors.component.html',
  styleUrls: ['./manage-contributors.component.css']
})
export class ManageContributorsComponent extends BaseModalComponent implements OnInit {
  fields = FIELDS
  studiesFields = STUDIES_FIELDS
  override headers = [ this.studiesFields.studyName, this.studiesFields.sponsor, this.studiesFields.role, this.studiesFields.startDate, this.studiesFields.endDate ]
  organizations!: any[]
  countries!: Country[]
  prefixes = PREFIX_TYPE_OPTIONS
  gradeTypes = GRADE_TYPE_OPTIONS

  form = new FormGroup({
    id: new FormControl(Guid.create().toString()),
    countryId: new FormControl('', [Validators.required]),
    prefix: new FormControl(0, [Validators.required]),
    firstname: new FormControl('', [Validators.required]),
    lastname: new FormControl('', [Validators.required]),
    grade: new FormControl(0, [Validators.required]),
    email: new FormControl('', [Validators.required]),
    primaryJobTitle: new FormControl(),
    secondaryJobTitle: new FormControl(),
    phone: new FormControl(),
    mobile: new FormControl(),
    publicData: new FormControl(true, [Validators.required]),
    dateOfConsent: new FormControl(new Date),
    consent: new FormControl(false, [Validators.required]),
    organizationId: new FormControl('', [Validators.required]),
    city: new FormControl('', [Validators.required]),
    status: new FormControl(true),
    remarks: new FormControl(NEW_RECORD),
  })

  constructor(private modal: NzModalRef,
    private commonService: CommonService,
    private service: ContributorService,
    private locationService: LocationService) {
    super();
  }
  
  ngOnInit() {   
    if(!this.isAdd && this.data) {
      this.form.controls.remarks.setValue(null)
      this.form.controls.remarks.addValidators([Validators.required])
      this.isLoading = true
      this.service.getById(this.data.id)
        .subscribe({
          next: (response) => {
            if(!response)
              this.commonService.showErrorMessage()
            this.form.patchValue(this.data = response)  
          },
          error: () => this.commonService.showErrorMessage(),
          complete: () => this.isLoading = false
        })
    }
  }

  onBlur(req: number) {
    switch(req) {
      case 1:
        if(this.form.value.firstname && this.form.value.lastname && (!this.isAdd ? this.form.value.firstname !== this.data.firstname || this.form.value.lastname !== this.data.lastname : true))
          this.service.checkIfNameExist(this.form.value.firstname, this.form.value.lastname).subscribe({
            next: (response) => {
              if(!response.isExist)
                return
              this.commonService.showErrorMessage(NAME_EXIST)
              this.form.controls.firstname.setErrors({ 'name-exist' : true })
              this.form.controls.lastname.setErrors({ 'name-exist' : true })
            },
            error: (error) => this.commonService.showErrorMessage(error)
          })
        break
      case 2:
        if(this.form.value.email && (!this.isAdd ? this.form.value.email !== this.data.email : true))
          this.service.checkIfEmailExist(this.form.value.email).subscribe({
            next: (response) => {
              if(!response.isExist)
                return
              this.commonService.showErrorMessage(EMAIL_EXIST)
              this.form.controls.email.setErrors({ 'email-exist' : true })
            },
            error: (error) => this.commonService.showErrorMessage(error)
          })
        break
    }
  }

  async onSubmit(isSave: boolean) {
    if(isSave) {
      if(this.form.valid) {
        this.isLoading = true
        var d = new Contributor().deserialize(this.form.getRawValue())
        d.location = await this.locationService.getSavedAddress()
        if(this.isAdd) 
          this.service.save(d)
            .subscribe({
              next: (response) => {
                if(response)
                  this.commonService.showMessage(ADD_CONTRIBUTOR_SUCCESS)
              },
              error: () => {
                this.commonService.showErrorMessage()
                this.isLoading = false
              },
              complete: () => {
                this.isLoading = false
                this.modal.destroy(true)
              },
            })
        else {
          this.service.update(d)
            .subscribe({
              next: (response) => {
                if(response)
                  this.commonService.showMessage(UPDATE_CONTRIBUTOR_SUCCESS)
              },
              error: () => {
                this.commonService.showErrorMessage()
                this.isLoading = false
              },
              complete: () => {
                this.isLoading = false
                if(!d.status && d.status != this.data?.status) 
                  this.service.remove(d).subscribe({
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
