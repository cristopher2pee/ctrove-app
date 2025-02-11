import { AfterViewInit, Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Guid } from 'guid-typescript';
import { NzModalRef, NzModalService } from 'ng-zorro-antd/modal';
import { ADD, ADD_SITE_SUCCESS, EDIT, INACTIVATE_ERROR, INVALID_INPUTS, NEW_RECORD, PHASE_PAGE, UPDATE_SITE_SUCCESS } from 'src/Utilities/common/app-strings';
import { SITES_FIELDS, Site, SitePhase } from 'src/app/models/dto/site';
import { BaseModalComponent } from 'src/app/pages/common/base-class';
import { CommonService } from 'src/app/services/common/common.service';
import { SitesService } from 'src/app/services/main/sites.service';
import { ManageSitePhaseComponent } from '../manage-site-phase/manage-site-phase.component';
import { DEF_MODAL_WIDTH } from 'src/Utilities/common/app-variables';
import { SITE_STATUS_OPTIONS } from 'src/Utilities/common/app-data';
import { StudyCountryService } from 'src/app/services/main/config/study-country.service';
import { ServiceTypeService } from 'src/app/services/main/config/service-type.service';
import { combineLatestWith } from 'rxjs';
import { StudyCountry } from 'src/app/models/dto/study-country';
import { ServiceType } from 'src/app/models/dto/service-type';

@Component({
  selector: 'app-manage-site',
  templateUrl: './manage-site.component.html',
  styleUrls: ['./manage-site.component.css']
})
export class ManageSiteComponent extends BaseModalComponent implements OnInit, AfterViewInit {
  fields = SITES_FIELDS
  siteStatuses = SITE_STATUS_OPTIONS
  studyCountries!: StudyCountry[]
  serviceTypes!: ServiceType[]
  sitePhases: SitePhase[] = []
  override data!: Site | null

  form = new FormGroup({
    id: new FormControl(Guid.create().toString()),
    code: new FormControl('', [Validators.required]),
    name: new FormControl('', [Validators.required]),
    studyCountryId: new FormControl('', [Validators.required]),
    serviceTypeId: new FormControl('', [Validators.required]),
    siteStatus: new FormControl(-1, [Validators.required]),
    date: new FormControl(this.commonService.setDateRange(), [Validators.required]),
    startDate: new FormControl(new Date, [Validators.required]),
    endDate: new FormControl(new Date, [Validators.required]),
    status: new FormControl(true),
    remarks: new FormControl(NEW_RECORD)
  });

  cForm = new FormGroup({
    search: new FormControl()
  })

  constructor(private service: SitesService,
    private modal: NzModalRef,
    private commonService: CommonService,
    private modalService: NzModalService,
    private studyCountryService: StudyCountryService,
    private serviceTypeService: ServiceTypeService) {
    super(); 
  }
    
  ngOnInit() {
    if(!this.isAdd && this.data) {
      this.form.controls.remarks.setValue(null)
      this.form.controls.remarks.addValidators([Validators.required])
      this.getData(this.data.id)
    }
  }

  getData = (id: string) => this.service.getById(id)
    .subscribe({
      next: (response) => {
        if(!response)
          this.commonService.showErrorMessage()
        this.form.patchValue(this.data = response)
        this.sitePhases = response.sitePhases
      },
      error: () => {
        this.commonService.showErrorMessage()
        this.isLoading = false
      },
      complete: () => this.isLoading = false
    })

  ngAfterViewInit() {
    this.form.controls.date.valueChanges.subscribe(response => {
      if(!response)
        return
      this.form.patchValue({
        startDate: response[0],
        endDate: response[1]
      })
    })

    this.studyCountryService.getDefList()
      .pipe(combineLatestWith(this.serviceTypeService.getDefList()))
      .subscribe({
        next: ([d1, d2]) => {
          this.studyCountries = d1.data
          this.serviceTypes = d2.data
        },
        error: () => this.commonService.showErrorMessage()
      })
  }

  manage(isAdd: boolean, data: SitePhase | null = null) {
    const modal: NzModalRef = this.modalService.create({
      nzTitle: `${ isAdd ? ADD : EDIT } ${PHASE_PAGE}`,
      nzContent: ManageSitePhaseComponent,
      nzWidth: DEF_MODAL_WIDTH,
      nzComponentParams: {
        addedPhases: this.sitePhases,
        data: data,
        isAdd: isAdd,
        sitesId: this.form.value.id
      },
      nzFooter: null
    })

    modal.afterClose.subscribe(response => {
      if(response === null)
        return

      if(response instanceof SitePhase) {
        let index = this.sitePhases.findIndex(d => d.id === response.id && d.phaseId === response.phaseId)
        if(index < 0)
          this.sitePhases.push(response)
        else
          this.sitePhases[index] = response
      }
      else {
        if(response && this.data)
          this.getData(this.data.id)
        // else 
        //   this.sitePhases.splice(this.sitePhases.findIndex(d => d.id === data?.id && d.phaseId === data.phaseId), 1)
      }
    })
  }

  submit(isAdd: boolean) {
    if(isAdd) {
      if(this.form.valid) {
        let d = new Site().deserialize(this.form.getRawValue())
        d.sitePhases = this.sitePhases
        if(this.isAdd) {
          this.service.save(d).subscribe({
            next: (response) => {
              if(!response)
                this.commonService.showErrorMessage()
              this.commonService.showMessage(ADD_SITE_SUCCESS)
            },
            error: () => this.commonService.showErrorMessage(),
            complete: () => {
              this.isLoading = false
              this.modal.destroy(true)
            }
          })
        }
        else {
          this.service.update(d).subscribe({
            next: (response) => {
              if(response)
                this.commonService.showMessage(UPDATE_SITE_SUCCESS)
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

  removePhase(id: string) {
    let index = this.sitePhases.findIndex(d => d.phaseId === id)

    if(index > -1)
      this.sitePhases.splice(index, 1)
  }

  onItemClicked(val: any) {
    console.log(val)
  }
}
