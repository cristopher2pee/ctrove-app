import { AfterViewInit, Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Guid } from 'guid-typescript';
import { NzModalRef } from 'ng-zorro-antd/modal';
import { ADD_ACCESS_SUCCESS, INACTIVATE_ERROR, INVALID_INPUTS, NEW_RECORD, NOTHING_CHANGED, RECORD_EXISTING, SOMETHING_WENT_WRONG, UPDATE_ACCESS_SUCCESS } from 'src/Utilities/common/app-strings';
import { ACCESS_FIELDS, Access, CustomAccess } from 'src/app/models/dto/access';
import { BaseModalComponent } from 'src/app/pages/common/base-class';
import { CommonService } from 'src/app/services/common/common.service';
import { AccessService } from 'src/app/services/main/access.service';
import { StudyCountryService } from 'src/app/services/main/config/study-country.service';
import { StudySitesService } from 'src/app/services/main/config/study-sites.service';
import { UserService } from 'src/app/services/main/user.service';

@Component({
  selector: 'app-manage-access',
  templateUrl: './manage-access.component.html',
  styleUrls: ['./manage-access.component.css']
})
export class ManageAccessComponent extends BaseModalComponent implements OnInit, AfterViewInit {
  fields = ACCESS_FIELDS
  users!: any[]
  countries!: any[]
  sites!: any[]
  access!: Access | null
  currentAccess!: CustomAccess
  userId!: string | null

  form = new FormGroup({
    id: new FormControl(Guid.create().toString()),
    userId: new FormControl('', [Validators.required]),
    countryId: new FormControl('', [Validators.required]),
    siteId: new FormControl(),
    read: new FormControl(true),
    write: new FormControl(false),
    bin: new FormControl(false),
    status: new FormControl(true),
    remarks: new FormControl(NEW_RECORD),
  })

  constructor(private modal: NzModalRef,
    private userService: UserService,
    private commonService: CommonService,
    private accessService: AccessService,
    private studyCountryService: StudyCountryService,
    private studySites: StudySitesService) {
    super();
  }

  ngOnInit() {
    if(this.isAdd && this.userId) {
      this.form.controls.userId.setValue(this.userId)
      this.getCountries()
    }

    if(!this.isAdd && this.access) {
      this.form.controls.remarks.setValue(null)
      this.form.controls.remarks.addValidators([Validators.required])
      this.isLoading = true
      this.accessService.getById(this.access.id)
        .subscribe({
          next: (response) => {        
            if(!response)
              this.commonService.showErrorMessage()

            this.currentAccess = response
            this.form.patchValue({
              id: response.accessResponse.id,
              userId: response.accessResponse.userId,
              countryId: response.sitesResponse != null ? response.sitesResponse.studyCountryId : response.studyCountry != null ? response.studyCountry.id : null,
              siteId: response.sitesResponse != null ? response.sitesResponse.id : null,
              write: response.accessResponse.rights === 3 || response.accessResponse.rights === 7,
              bin: response.accessResponse.rights === 5 || response.accessResponse.rights === 7,
              status: response.accessResponse.status
            })
            if(this.form.value.countryId) {
              this.getCountries()
              this.getSites(this.form.value.countryId)
            }
          },
          error: () => this.commonService.showErrorMessage(SOMETHING_WENT_WRONG),
          complete: () => this.isLoading = false
        })
    }
  }

  ngAfterViewInit() {
    this.userService.getDefList().subscribe({
      next: (response) => this.users = response.data.map(u => ({
        id: u.id,
        name: this.commonService.formatName(u)
      })),
      error: () => this.commonService.showErrorMessage()
    })

    this.form.valueChanges
      .subscribe(changes =>
        this.hasChanged = (changes.userId !== this.currentAccess?.accessResponse.userId) 
          || ((this.currentAccess.studyCountry !== null && changes.countryId != this.currentAccess.studyCountry.id) || (this.currentAccess.sitesResponse !== null && changes.countryId !== this.currentAccess.sitesResponse.studyCountryId))
          || (this.currentAccess.sitesResponse !== null && changes.siteId !== this.currentAccess?.sitesResponse.id || this.currentAccess.sitesResponse == null)
          || (this.commonService.getTotalRights(changes.read, changes.write, changes.bin) !== this.currentAccess.accessResponse.rights)
          || (this.currentAccess.accessResponse.status !== changes.status))
  }

  getCountries() {
    this.form.markAsTouched()
    if(this.countries)
      return

    this.studyCountryService.getDefList().subscribe({
      next: (response) => this.countries = response.data,
      error: () => this.commonService.showErrorMessage()
    })
  }

  getSites = (id: string) => 
    this.studySites.getDefList(id).subscribe({
      next: (response) => this.sites = response.data,
      error: () => this.commonService.showErrorMessage()
    })

  submit(isSave: boolean) {
    if(isSave) {
      if(this.form.valid) {
        if(!this.hasChanged) {
          this.commonService.showErrorMessage(NOTHING_CHANGED)
          return
        }

        this.isLoading = true
        let d = new Access().deserialize(this.form.getRawValue())
        d.setAccessLevel() // Assign Access Level
        d.setRights() // Assign Rights 

        this.accessService.isExisting(d).subscribe({
          next: (response) => {
            if(!response) {
              if(this.isAdd) 
                this.accessService.save(d).subscribe({
                  next: (response) => {
                    if(!response)
                      this.commonService.showErrorMessage()
                    this.commonService.showMessage(ADD_ACCESS_SUCCESS)
                  },
                  error: () => {
                    this.commonService.showErrorMessage()
                    this.isLoading = false
                  },
                  complete: () => {
                    this.isLoading = false
                    this.modal.destroy(true)
                  }
                })
              else 
                this.accessService.update(d).subscribe({
                  next: (response) => {
                    if(!response)
                      this.commonService.showErrorMessage()
                    this.commonService.showMessage(UPDATE_ACCESS_SUCCESS)
                  },
                  error: () => {
                    this.commonService.showErrorMessage()
                    this.isLoading = false
                  },
                  complete: () => {
                    this.isLoading = false
                    if(!d.status && d.status != this.currentAccess?.accessResponse.status) 
                      this.accessService.remove(d).subscribe({
                        error: () => this.commonService.showErrorMessage(INACTIVATE_ERROR)
                      })
                    this.modal.destroy(true)
                  }
                })
            }
            else
              this.commonService.showErrorMessage(RECORD_EXISTING)
          },
          error: () => {
            this.commonService.showErrorMessage()
            this.isLoading = false
          },
          complete: () => this.isLoading = true
        })
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
