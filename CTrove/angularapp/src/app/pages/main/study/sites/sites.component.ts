import { AfterViewInit, Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { NzModalRef, NzModalService } from 'ng-zorro-antd/modal';
import { ADD, DEFAULT_EMPTY_STRING, EDIT, SITES_PAGE } from 'src/Utilities/common/app-strings';
import { LARGE_MODAL_WIDTH } from 'src/Utilities/common/app-variables';
import { SITES_FIELDS, Site, SitesFilter } from 'src/app/models/dto/site';
import { BaseMasterListComponent } from 'src/app/pages/common/base-class';
import { CommonService } from 'src/app/services/common/common.service';
import { SitesService } from 'src/app/services/main/sites.service';
import { ManageSiteComponent } from './manage-site/manage-site.component';
import { ServiceTypeService } from 'src/app/services/main/config/service-type.service';
import { StudyCountryService } from 'src/app/services/main/config/study-country.service';
import { combineLatestWith } from 'rxjs';
import { SITE_STATUS_OPTIONS } from 'src/Utilities/common/app-data';
import { ServiceType } from 'src/app/models/dto/service-type';
import { StudyCountry } from 'src/app/models/dto/study-country';

@Component({
  selector: 'app-sites',
  templateUrl: './sites.component.html',
  styleUrls: ['./sites.component.css']
})
export class SitesComponent extends BaseMasterListComponent implements AfterViewInit {
  fields = SITES_FIELDS
  override headers = [this.fields.code, this.fields.name, this.fields.studyCountry, this.fields.startDate, this.fields.endDate, this.fields.serviceType, this.fields.siteStatus, this.fields.phase, this.fields.status]
  siteStatuses = SITE_STATUS_OPTIONS
  serviceTypes!: ServiceType[]
  studyCountries!: StudyCountry[]

  form = new FormGroup ({
    search: new FormControl(),
    status: new FormControl(true),
    siteStatusId: new FormControl(),
    serviceTypeId: new FormControl(),
    studyCountryId: new FormControl()
  })

  constructor(private service: SitesService,
    private serviceTypeService: ServiceTypeService,
    private studyCountryService: StudyCountryService,
    private modalService: NzModalService,
    private commonService: CommonService) {
    super()
  }

  ngAfterViewInit() {
    this.getList()

    this.serviceTypeService.getDefList()
      .pipe(combineLatestWith(this.studyCountryService.getDefList()))
      .subscribe(([d1, d2]) => {
        this.serviceTypes = d1.data
        this.studyCountries = d2.data
      })

    this.form.valueChanges.subscribe(_ => this.getList(this.meta.page, this.meta.limit))
  }
  
  getList = (page: number = this.meta.page, limit: number = this.meta.limit) =>
    this.service.getList(page, limit, new SitesFilter().deserialize(this.form.getRawValue())).subscribe({
      next: (response) => {
        this.meta = response.meta
        this.data = response.data.map(d => ({
          id: d.id,
          siteCode: d.code,
          siteName: d.name,
          studyCountry: d.studyCountry.name,
          startDate: this.commonService.formatDate(d.startDate),
          endDate: this.commonService.formatDate(d.endDate),
          serviceType: d.serviceType.name,
          siteStatus: this.commonService.formatSiteStatus(d.siteStatus),
          phase: d.sitePhases && d.sitePhases.length > 0 ? d.sitePhases[0].phase.name : DEFAULT_EMPTY_STRING,
          status: this.commonService.formatStatus(d.status)
        }))

        if(this.meta.page <= 0)
          this.meta.page = 1
      },
      error: () => this.isLoading = false,
      complete: () => this.isLoading = false
    })

  tableChanged = (val: any) => this.getList(val.pageIndex, val.pageSize)

  manage(isAdd: boolean, site: Site | null = null) {
    const modal: NzModalRef = this.modalService.create({
      nzTitle: `${ isAdd ? ADD : EDIT } ${SITES_PAGE}`,
      nzContent: ManageSiteComponent,
      nzWidth: LARGE_MODAL_WIDTH,
      nzComponentParams: {
        isAdd: isAdd,
        data: site
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
