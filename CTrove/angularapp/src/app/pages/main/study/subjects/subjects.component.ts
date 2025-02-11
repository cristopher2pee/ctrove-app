import { AfterViewInit, Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { NzModalRef, NzModalService } from 'ng-zorro-antd/modal';
import { combineLatestWith } from 'rxjs';
import { ADD, EDIT, NO_ASSIGNED_SITE, SUBJECTS_PAGE } from 'src/Utilities/common/app-strings';
import { SUBJECT_FIELDS, Subject, SubjectFilter } from 'src/app/models/dto/subject';
import { BaseMasterListComponent } from 'src/app/pages/common/base-class';
import { SubjectService } from 'src/app/services/main/subject.service';
import { ManageSubjectComponent } from './manage-subject/manage-subject.component';
import { LARGE_MODAL_WIDTH } from 'src/Utilities/common/app-variables';
import { CommonService } from 'src/app/services/common/common.service';
import { StudyCountryService } from 'src/app/services/main/config/study-country.service';
import { SitesService } from 'src/app/services/main/sites.service';
import { StudyCountry } from 'src/app/models/dto/study-country';
import { Site } from 'src/app/models/dto/site';
import { SUBJECT_STATUS_OPTIONS } from 'src/Utilities/common/app-data';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-subjects',
  templateUrl: './subjects.component.html',
  styleUrls: ['./subjects.component.css']
})
export class SubjectsComponent extends BaseMasterListComponent implements AfterViewInit, OnInit {
  fields = SUBJECT_FIELDS
  override headers = [this.fields.country, this.fields.site, this.fields.screenNo, this.fields.randomization, this.fields.yearOfBirth, this.fields.sex, this.fields.ethnicity, this.fields.race, this.fields.lastVisit, this.fields.nextVisit, this.fields.subjectStatus]
  countries!: StudyCountry[]
  sites!: Site[]
  subjectStatuses = SUBJECT_STATUS_OPTIONS
  hasAccess: boolean = false
  isLoadingAccess: boolean = false

  form = new FormGroup({
    search: new FormControl(),
    status: new FormControl(true),
    ethnicityIds: new FormControl(),
    raceIds: new FormControl(),
    sitesIds: new FormControl(),
    subjectStatus: new FormControl(),
    studyCountryIds: new FormControl()
  })

  constructor(private service: SubjectService,
    private commonService: CommonService,
    private countryService: StudyCountryService,
    private siteService: SitesService,
    private accountService: AccountService,
    private modal: NzModalService) {
    super();
  }

  ngOnInit() {
    this.countryService.getDefList()
      .pipe(combineLatestWith(this.siteService.getDefList()))
      .subscribe({
        next: async ([d1, d2]) => {
          let user = await this.accountService.getUserProfile()
          this.countries = d1.data.filter(d => user.studyCountryListResponse.some(c => c.id === d.id))
          this.sites = d2.data.filter(d => user.sitesListResponse.some(s => s.id === d.id))
        },
        error: () => this.commonService.showErrorMessage()
      })
  }

  ngAfterViewInit() {
    this.getList()
    this.form.valueChanges
      .subscribe(() => this.getList(this.meta.page, this.meta.limit))
    this.checkAccess()
  }

  getList = (page: number = this.meta.page, limit: number = this.meta.limit) => 
    this.service.getList(page, limit, new SubjectFilter().deserialize(this.form.getRawValue())).subscribe({
      next: (response) => {
        this.meta = response.meta
        this.data = response.data.map(d => ({
          id: d.id,
          country: d.sites.studyCountry.name,
          site: d.sites.name,
          screeningNo: d.screeningNo,
          randNo: d.randNo,
          yearOfBirth: d.yearOfBirth,
          sex: this.commonService.formatGender(d.sex),
          ethnicity: d.ethnicity.name,
          race: d.race.name,
          lastVisit: '',
          nextVisit: '',
          subjectStatus: this.commonService.formatSubjectStatus(d.subjectStatus)
        }))

        if(this.meta.page <= 0)
          this.meta.page = 1
      },
      error: () => this.isLoading = false,
      complete: () => this.isLoading = false
    })

  tableChanged = (val: any) => this.getList(val.pageIndex, val.pageSize)

  async manage(isAdd: boolean, d: Subject | null = null) {
    if(isAdd && !(await this.checkAccess())) {
      this.commonService.showErrorMessage(NO_ASSIGNED_SITE)
      return
    }
    
    const modal: NzModalRef = this.modal.create({
      nzTitle: `${ isAdd ? ADD : EDIT } ${SUBJECTS_PAGE}`,
      nzContent: ManageSubjectComponent,
      nzWidth: LARGE_MODAL_WIDTH,
      nzComponentParams: {
        isAdd: isAdd,
        data: d,
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

  async checkAccess() {
    let response = await this.accountService.getUserProfile(true)
    if(!response)
      return
    return this.hasAccess = response.sitesListResponse.length > 0
  }
}
