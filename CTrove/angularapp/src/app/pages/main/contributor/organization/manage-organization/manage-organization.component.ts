import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Guid } from 'guid-typescript';
import { NzModalRef } from 'ng-zorro-antd/modal';
import { combineLatestWith } from 'rxjs';
import { ADD_ORGANIZATION_SUCCESS, COMPANY_EXIST, INACTIVATE_ERROR, INVALID_INPUTS, NEW_RECORD, UPDATE_ORGANIZATION_SUCCESS } from 'src/Utilities/common/app-strings';
import { ContactType } from 'src/app/models/contributor/contact-type';
import { Country } from 'src/app/models/contributor/country';
import { FIELDS, Organization } from 'src/app/models/contributor/organization';
import { VendorType } from 'src/app/models/contributor/vendor-type';
import { BaseModalComponent } from 'src/app/pages/common/base-class';
import { CommonService } from 'src/app/services/common/common.service';
import { LocationService } from 'src/app/services/common/location.service';
import { ContactTypeService } from 'src/app/services/main/contributor/contact-type.service';
import { ContributorService } from 'src/app/services/main/contributor/contributor.service';
import { OrganizationService } from 'src/app/services/main/contributor/organization.service';
import { VendorTypeService } from 'src/app/services/main/contributor/vendor-type.service';

@Component({
  selector: 'app-manage-organization',
  templateUrl: './manage-organization.component.html',
  styleUrls: ['./manage-organization.component.css']
})
export class ManageOrganizationComponent extends BaseModalComponent implements OnInit {
  fields = FIELDS
  contactTypes!: ContactType[]
  parentTypes!: any[]
  vendorTypes!: VendorType[]
  countries!: Country[]
  contributors1: any[] = []
  contributors2: any[] = []

  form = new FormGroup({
    id: new FormControl(Guid.create().toString()),
    companyName: new FormControl('', [Validators.required]),
    contactTypeId: new FormControl('', [Validators.required]),
    parent: new FormControl(0, [Validators.required]),
    vendorTypeId: new FormControl('', [Validators.required]),
    email: new FormControl(),
    website: new FormControl(),
    countryId: new FormControl('', [Validators.required]),
    state: new FormControl(),
    city: new FormControl(),
    zipCode: new FormControl(),
    phoneNumber: new FormControl(),
    faxNumber: new FormControl(),
    addressLine1: new FormControl(),
    addressLine2: new FormControl(),
    addressLine3: new FormControl(),
    primaryContactId: new FormControl(),
    secondaryContactId: new FormControl(),
    notes: new FormControl(),
    status: new FormControl(true),
    remarks: new FormControl(NEW_RECORD),
  })

  constructor(private modal: NzModalRef,
    private commonService: CommonService,
    private service: OrganizationService,
    private contriService: ContributorService,
    private contactService: ContactTypeService,
    private vendorService: VendorTypeService,
    private locationService: LocationService) {
    super();
  }
  
  ngOnInit() {   
    this.contactService.getListResources()
      .pipe(combineLatestWith(this.vendorService.getListResources()))
      .subscribe({
        next: ([d1, d2]) => {
          this.contactTypes = d1.data
          this.vendorTypes = d2.data
        },
        error: (error) => this.commonService.showErrorMessage(error)
      })

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
            this.contributors1 = this.formatContributorsOption([this.data.primaryContributorContact])
            this.contributors2 = this.formatContributorsOption([this.data.secondaryContributorContact])
          },
          error: () => this.commonService.showErrorMessage(),
          complete: () => this.isLoading = false
        })
    }
  }

  onBlur(req: number) {
    switch(req) {
      case 1:
        if(this.form.value.companyName && (!this.isAdd ? this.form.value.companyName !== this.data.companyName : true))
          this.service.checkIfCompanyExist(this.form.value.companyName).subscribe({
            next: (response) => {
              if(!response.isExist)
                return
              this.commonService.showErrorMessage(COMPANY_EXIST)
              this.form.controls.companyName.setErrors({ 'company-exist' : true })
            },
            error: (error) => this.commonService.showErrorMessage(error)
          })
        break
    }
  }

  onSearch(req: number, val: string) {
    switch(req) {
      case 1:
      case 2:
        if(!val)
          return
        this.contriService.search(val).subscribe({
          next: (response) => {
            if(req === 1)
              this.contributors1 = this.formatContributorsOption(response.
                filter(d => d.id !== this.data.secondaryContactId))
            else
              this.contributors2 = this.formatContributorsOption(response.
                filter(d => d.id !== this.data.primaryContactId))
          },
          error: (error) => this.commonService.showErrorMessage(error)
        })
      break
    }
  }

  private formatContributorsOption = (options: any[]) => options.map(d => ({
      id: d.id,
      name: this.commonService.formatNames(d.firstname, d.lastname)
    }))

  async onSubmit(isSave: boolean) {
    if(isSave) {
      if(this.form.valid) {
        this.isLoading = true
        var d = new Organization().deserialize(this.form.getRawValue())
        d.location = await this.locationService.getSavedAddress()
        if(this.isAdd) 
          this.service.save(d)
            .subscribe({
              next: (response) => {
                if(response)
                  this.commonService.showMessage(ADD_ORGANIZATION_SUCCESS)
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
                  this.commonService.showMessage(UPDATE_ORGANIZATION_SUCCESS)
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
