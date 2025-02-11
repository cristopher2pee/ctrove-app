import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { STUDY_TYPE_OPTIONS } from 'src/Utilities/common/app-data';
import { ADD_STUDY_SUCCESS, ETHNICITY_PAGE, INVALID_INPUTS, PHASE_PAGE, RACE_PAGE, SERVICE_TYPE_PAGE, STUDY_COUNTRY_PAGE, THERAPEUTIC_AREA_PAGE, TRIAL_CLASSIFICATION_PAGE, UPDATE_STUDY_SUCCESS, VISIT_PAGE } from 'src/Utilities/common/app-strings';
import { STUDY_FIELDS, Study } from 'src/app/models/dto/study';
import { TherapeuticArea } from 'src/app/models/dto/therapeutic-area';
import { TrialClassification } from 'src/app/models/dto/trial-classification';
import { BaseComponent } from 'src/app/pages/common/base-class';
import { CommonService } from 'src/app/services/common/common.service';
import { StudyService } from 'src/app/services/main/config/study.service';
import { TrialClassificationComponent } from './trial-classification/trial-classification.component';
import { firstValueFrom, timer } from 'rxjs';
import { TherapeuticAreaComponent } from './therapeutic-area/therapeutic-area.component';
import { SubNavigationService } from 'src/app/services/main/sub-navigation.service';
import { Guid } from 'guid-typescript';
import { LocationService } from 'src/app/services/common/location.service';
import { NzDrawerService } from 'ng-zorro-antd/drawer';
import { AuditTrailComponent } from 'src/app/pages/common/drawers/audit-trail/audit-trail.component';

@Component({
  selector: 'app-config',
  templateUrl: './config.component.html',
  styleUrls: ['./config.component.css']
})
export class ConfigComponent extends BaseComponent implements OnInit, AfterViewInit {

  @ViewChild('trialClassificationTable')
  trialClassificationTable!: TrialClassificationComponent

  @ViewChild('therapeuticAreaTable')
  therapeuticAreaTable!: TherapeuticAreaComponent

  fields = STUDY_FIELDS
  studyTypes = STUDY_TYPE_OPTIONS
  trialClassifications!: TrialClassification[]
  therapeuticAreas!: TherapeuticArea[]
  tables = [THERAPEUTIC_AREA_PAGE, TRIAL_CLASSIFICATION_PAGE, STUDY_COUNTRY_PAGE, SERVICE_TYPE_PAGE, PHASE_PAGE, VISIT_PAGE, ETHNICITY_PAGE, RACE_PAGE]
  showClassification = false
  showTherapeutic = true
  currentStudy!: Study
  canModify = false

  form = new FormGroup({
    id: new FormControl(Guid.create().toString()),
    name: new FormControl('', [Validators.required]),
    code: new FormControl('', [Validators.required]),
    therapeuticAreaId: new FormControl('', [Validators.required]),
    studyType: new FormControl('', [Validators.required]),
    sponsor: new FormControl('', [Validators.required]),
    billingCode: new FormControl('', [Validators.required]),
    classificationId: new FormControl('', [Validators.required]),
    remarks: new FormControl('', [Validators.required])
  })

  constructor(private studyService: StudyService,
    private commonService: CommonService,
    private subNavService: SubNavigationService,
    private locationService: LocationService,
    private drawerService: NzDrawerService) {
    super();
  }

  ngOnInit() {
    this.load()
  }

  async ngAfterViewInit() {
    await firstValueFrom(timer(500))
    this.subNavService.updateSubPath('Details')
  }

  load() {
    this.studyService.getDefList().subscribe({
      next: (response) => {
        if(response.data.length > 0)
          this.form.patchValue(this.currentStudy = response.data[0])
      },
      error: (error) => {
        this.commonService.showErrorMessage()
      },
      complete: () => this.form.markAsUntouched()
    })
  }

  async onSubmit() {
    if(this.form.valid) {
      this.isLoading = true
      var study = new Study().deserialize(this.form.getRawValue())
      study.location = await this.locationService.getSavedAddress()
      if(!this.currentStudy) 
        this.studyService.save(study).subscribe({
          next: (response) => {
            if(!response)
              this.commonService.showErrorMessage()
            this.commonService.showMessage(ADD_STUDY_SUCCESS)
            this.load()
          },
          error: () => {
            this.commonService.showErrorMessage()
            this.isLoading = false
          },
          complete: () => this.isLoading = false
        })
      else
        this.studyService.update(study).subscribe({
          next: (response) => {
            if(!response)
              this.commonService.showErrorMessage()
            this.commonService.showMessage(UPDATE_STUDY_SUCCESS)
            this.load()
          },
          error: () => {
            this.commonService.showErrorMessage()
            this.isLoading = false
          },
          complete: () => this.isLoading = false
        })
    }
    else {
      this.form.markAllAsTouched()
      this.commonService.showErrorMessage(INVALID_INPUTS)
    }
  }

  scrollTo = async (req: number, e: string) => {
    document.getElementById(e)?.scrollIntoView();

    await firstValueFrom(timer(500));

    switch(req) {
      case 1: this.showClassification = true; break
      case 2: this.showTherapeutic = true; break
    }

    await firstValueFrom(timer(500));

    switch(req) {
      case 1: this.trialClassificationTable.manage(true); break
      case 2: this.therapeuticAreaTable.manage(true); break
    }
  }

  showAudit() {
    if(!this.form.value.id)
    return

    this.drawerService.create<AuditTrailComponent, { value: string }, string>({
      // nzFooter: 'Footer',
      nzContent: AuditTrailComponent,
      nzContentParams: {
        id: this.form.value.id
      }
    })
  }
}
