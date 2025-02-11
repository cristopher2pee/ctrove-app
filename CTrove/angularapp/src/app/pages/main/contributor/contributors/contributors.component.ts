import { AfterViewInit, Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { NzModalRef, NzModalService } from 'ng-zorro-antd/modal';
import { combineLatestWith } from 'rxjs';
import { ContributorFilter, FIELDS } from 'src/app/models/contributor/contributor';
import { Country } from 'src/app/models/contributor/country';
import { Organization } from 'src/app/models/contributor/organization';
import { BaseMasterListComponent } from 'src/app/pages/common/base-class';
import { CommonService } from 'src/app/services/common/common.service';
import { ContributorService } from 'src/app/services/main/contributor/contributor.service';
import { CountryService } from 'src/app/services/main/contributor/country.service';
import { OrganizationService } from 'src/app/services/main/contributor/organization.service';
import { ManageContributorsComponent } from './manage-contributors/manage-contributors.component';
import { ADD, CONTRIBUTOR, EDIT } from 'src/Utilities/common/app-strings';
import { LARGE_MODAL_WIDTH } from 'src/Utilities/common/app-variables';

@Component({
  selector: 'app-contributors',
  templateUrl: './contributors.component.html',
  styleUrls: ['./contributors.component.css']
})
export class ContributorsComponent extends BaseMasterListComponent implements OnInit, AfterViewInit {
  fields = FIELDS
  override headers = [this.fields.prefix, this.fields.lastName, this.fields.firstName, this.fields.grade, this.fields.email, this.fields.organization]
  countries!: Country[]
  organizations!: any[]

  form = new FormGroup ({
    search: new FormControl(),
    countrysId: new FormControl(),
    organizationsId: new FormControl(),
    status: new FormControl(true)
  })

  constructor(private service: ContributorService,
    private modalService: NzModalService,
    private commonService: CommonService,
    private countryService: CountryService,
    private organizationService: OrganizationService) {
    super();
  }

  ngOnInit() {
    this.countryService.getListResources()
      .pipe(combineLatestWith(this.organizationService.getListResources()))
      .subscribe({
        next: ([d1, d2]) => {
          this.countries = d1.data
          this.organizations = d2.data.map(d => ({
            id: d.id,
            name: d.companyName
          }))
        },
        error: (error) => this.commonService.showErrorMessage(error)
      })
  }

  ngAfterViewInit() {
    this.getList(this.meta.page, this.meta.limit)

    this.form.valueChanges
      .subscribe(_ => this.getList(this.meta.page, this.meta.limit))
  }

  getList = (page: number = this.meta.page, limit: number = this.meta.limit) => this.service.getList(page, limit, new ContributorFilter().deserialize(this.form.getRawValue())).subscribe({
    next: (response) => {
      this.meta = response.meta
      this.data = response.data.map(d => ({
        id: d.id,
        prefix: this.commonService.formatPrefixType(d.prefix),
        lastName: d.lastname,
        firstName: d.firstname,
        grade: this.commonService.formatGradeType(d.grade),
        email: d.email,
        organization: d.organization ? d.organization.companyName : null
      }))

      if(this.meta.page <= 0)
        this.meta.page = 1
    },
    error: () => this.isLoading = false,
    complete: () => this.isLoading = false
  })
  
  tableChanged = (val: any) => this.getList(val.pageIndex, val.pageSize)

  manage(isAdd: boolean, d: Organization | null = null) {
    const modal: NzModalRef = this.modalService.create({
      nzTitle: `${ isAdd ? ADD : EDIT } ${CONTRIBUTOR}`,
      nzContent: ManageContributorsComponent,
      nzWidth: LARGE_MODAL_WIDTH,
      nzComponentParams: {
        isAdd: isAdd,
        data: d,
        countries: this.countries,
        organizations: this.organizations
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