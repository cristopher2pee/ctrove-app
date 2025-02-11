import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Guid } from 'guid-typescript';
import { NzModalRef, NzModalService } from 'ng-zorro-antd/modal';
import { combineLatestWith } from 'rxjs';
import { GENDER_OPTIONS, SUBJECT_STATUS_OPTIONS } from 'src/Utilities/common/app-data';
import { ADD, ADD_SUBJECT_SUCCESS, EDIT, INACTIVATE_ERROR, INVALID_INPUTS, NEW_RECORD, PHASE_PAGE, UPDATE_SUBJECT_SUCCESS } from 'src/Utilities/common/app-strings';
import { DEF_MODAL_WIDTH } from 'src/Utilities/common/app-variables';
import { Ethnicity } from 'src/app/models/dto/ethnicity';
import { Race } from 'src/app/models/dto/race';
import { SUBJECT_FIELDS, Subject, SubjectPhase } from 'src/app/models/dto/subject';
import { BaseModalComponent } from 'src/app/pages/common/base-class';
import { CommonService } from 'src/app/services/common/common.service';
import { LocationService } from 'src/app/services/common/location.service';
import { EthnicityService } from 'src/app/services/main/config/ethnicity.service';
import { RaceService } from 'src/app/services/main/config/race.service';
import { SubjectService } from 'src/app/services/main/subject.service';
import { ManageSubjectPhaseComponent } from '../manage-subject-phase/manage-subject-phase.component';
import { LocalStorageService } from 'src/app/services/local-storage.service';
import { USER_SITE } from 'src/Utilities/common/app-local-storage-keys';
import { Site } from 'src/app/models/dto/site';
import { StudyCountry } from 'src/app/models/dto/study-country';

@Component({
  selector: 'app-manage-subject',
  templateUrl: './manage-subject.component.html',
  styleUrls: ['./manage-subject.component.css']
})
export class ManageSubjectComponent extends BaseModalComponent implements OnInit {
  fields = SUBJECT_FIELDS
  override data!: Subject | null
  genderOptions = GENDER_OPTIONS
  subjectStatuses = SUBJECT_STATUS_OPTIONS
  ethnicityOptions!: Ethnicity[]
  raceOptions!: Race[]
  subjectPhases: SubjectPhase[] = []
  userSite!: Site
  siteCountry!: StudyCountry

  form = new FormGroup({
    id: new FormControl(Guid.create().toString()),
    screeningNo: new FormControl('', [Validators.required]),
    randNo: new FormControl('', [Validators.required]),
    date: new FormControl(this.commonService.getDate(18), [Validators.required]),
    yearOfBirth: new FormControl(this.commonService.getDate(18).getFullYear(), [Validators.required]),
    age: new FormControl(18, [Validators.required]),
    sex: new FormControl(0, [Validators.required]),
    ethnicityId: new FormControl('', [Validators.required]),
    raceId: new FormControl('', [Validators.required]),
    subjectStatus: new FormControl(0, [Validators.required]),
    status: new FormControl(true),
    remarks: new FormControl(NEW_RECORD),
    sitesId: new FormControl(),
  })

  constructor(private service: SubjectService,
    private modal: NzModalRef,
    private modalService: NzModalService,
    private ethnicityService: EthnicityService,
    private raceService: RaceService,
    private commonService: CommonService,
    private locationService: LocationService,
    private localService: LocalStorageService) {
    super();
  }

  ngOnInit() {
    this.form.controls.date.valueChanges.subscribe(date => {
      if(!date)
        return
      let selectedYear = date.getFullYear()
      this.form.controls.age.setValue(new Date().getFullYear() - selectedYear)
      this.form.controls.yearOfBirth.setValue(selectedYear)
    })

    this.ethnicityService.getDefList()
      .pipe(combineLatestWith(this.raceService.getDefList()))
      .subscribe(([d1, d2]) => {
        this.ethnicityOptions = d1.data
        this.raceOptions = d2.data
      })

    if(!this.isAdd && this.data) {
      this.form.controls.remarks.setValue(null)
      this.form.controls.remarks.addValidators([Validators.required])
      this.service.getById(this.data.id)
        .subscribe({
          next: (response) => {
            if(!response)
              this.commonService.showErrorMessage()
            this.form.patchValue(response)  

            this.userSite = response.sites
            this.siteCountry = response.sites.studyCountry
            this.subjectPhases = response.subjectPhases
          },
          error: () => {
            this.commonService.showErrorMessage()
            this.isLoading = false
          },
          complete: () => this.isLoading = false
        })
    }
    else {
      let savedSite = this.localService.getData(USER_SITE) as Site

      if(!savedSite)
        return

      this.userSite = savedSite
      this.siteCountry = savedSite.studyCountry
      this.form.controls.sitesId.setValue(savedSite.id)
    }
  }

  getData = (id: string) => this.service.getById(id)
    .subscribe({
      next: (response) => {
        if(!response)
          this.commonService.showErrorMessage()
        this.form.patchValue(this.data = response)
        this.subjectPhases = response.subjectPhases
      },
      error: () => {
        this.commonService.showErrorMessage()
        this.isLoading = false
      },
      complete: () => this.isLoading = false
    })

  manage(isAdd: boolean, data: SubjectPhase | null = null) {
    const modal: NzModalRef = this.modalService.create({
      nzTitle: `${ isAdd ? ADD : EDIT } ${PHASE_PAGE}`,
      nzContent: ManageSubjectPhaseComponent,
      nzWidth: DEF_MODAL_WIDTH,
      nzComponentParams: {
        addedPhases: this.subjectPhases,
        data: data,
        isAdd: isAdd
      },
      nzFooter: null
    })

    modal.afterClose.subscribe(response => {
      if(response === null)
        return

      if(response instanceof SubjectPhase) {
        let index = this.subjectPhases.findIndex(d => d.id === response.id && d.phaseId === response.phaseId)
        if(index < 0)
          this.subjectPhases.push(response)
        else
          this.subjectPhases[index] = response
      }
      else {
        if(response && this.data)
          this.getData(this.data.id)
        // else 
        //   this.sitePhases.splice(this.sitePhases.findIndex(d => d.id === data?.id && d.phaseId === data.phaseId), 1)
      }
    })
  }

  async submit(isSave: boolean) {
    if(isSave) {
      if(this.form.valid) {
        this.isLoading = true
        let d = new Subject().deserialize(this.form.getRawValue())
        d.subjectPhases = this.subjectPhases
        d.location = await this.locationService.getSavedAddress()
        if(this.isAdd) 
          this.service.save(d).subscribe({
            next: (response) => {
              if(!response)
                this.commonService.showErrorMessage()
              this.commonService.showMessage(ADD_SUBJECT_SUCCESS)
            },
            error: () => this.commonService.showErrorMessage(),
            complete: () => {
              this.isLoading = false
              this.modal.destroy(true)
            }
          })
        else
          this.service.update(d).subscribe({
            next: (response) => {
              if(!response)
                this.commonService.showErrorMessage()
              this.commonService.showMessage(UPDATE_SUBJECT_SUCCESS)
            },
            error: () => this.commonService.showErrorMessage(),
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
      else {
        this.form.markAllAsTouched()
        this.commonService.showErrorMessage(INVALID_INPUTS)
      }
    }
    else 
      this.modal.destroy()
  }
}
