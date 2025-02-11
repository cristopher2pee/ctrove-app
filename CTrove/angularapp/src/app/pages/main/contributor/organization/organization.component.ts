import { AfterViewInit, Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { NzModalRef, NzModalService } from 'ng-zorro-antd/modal';
import { ADD, EDIT, ORGANIZATION } from 'src/Utilities/common/app-strings';
import { FIELDS, Organization, OrganizationFilter } from 'src/app/models/contributor/organization';
import { BaseMasterListComponent } from 'src/app/pages/common/base-class';
import { OrganizationService } from 'src/app/services/main/contributor/organization.service';
import { ManageOrganizationComponent } from './manage-organization/manage-organization.component';
import { LARGE_MODAL_WIDTH } from 'src/Utilities/common/app-variables';
import { CountryService } from 'src/app/services/main/contributor/country.service';
import { Country } from 'src/app/models/contributor/country';
import { PARENT_TYPE_OPTIONS } from 'src/Utilities/common/app-data';
import { CommonService } from 'src/app/services/common/common.service';

@Component({
  selector: 'app-organization',
  templateUrl: './organization.component.html',
  styleUrls: ['./organization.component.css']
})
export class OrganizationComponent extends BaseMasterListComponent implements OnInit, AfterViewInit {
  fields = FIELDS
  override headers = [this.fields.name, this.fields.parent, this.fields.contactType, this.fields.vendorType, this.fields.country, this.fields.status, this.fields.primaryContact]
  countries!: Country[]
  parentTypes = PARENT_TYPE_OPTIONS

  form = new FormGroup ({
    search: new FormControl(),
    countrysId: new FormControl(),
    parentTypes: new FormControl(),
    status: new FormControl(true)
  })

  constructor(private service: OrganizationService,
    private modalService: NzModalService,
    private commonService: CommonService,
    private countryService: CountryService) {
    super();
  }

  ngOnInit() {
    this.countryService.getListResources().subscribe({
      next: (response) => this.countries = response.data,
      error: (error) => this.commonService.showErrorMessage(error)
    })
  }

  ngAfterViewInit() {
    this.getList(this.meta.page, this.meta.limit)

    this.form.valueChanges
      .subscribe(_ => this.getList(this.meta.page, this.meta.limit))
  }

  getList = (page: number = this.meta.page, limit: number = this.meta.limit) => this.service.getList(page, limit, new OrganizationFilter().deserialize(this.form.getRawValue())).subscribe({
    next: (response) => {
      this.meta = response.meta
      this.data = response.data.map(d => ({
        id: d.id,
        name: d.companyName,
        parent: this.commonService.formatParentType(d.parent),
        contactType: d.contactType.name,
        vendorType: d.vendorType.name,
        country: d.country.name,
        status: this.commonService.formatStatus(d.status),
        primaryContact: d.primaryContributorContact ? d.primaryContributorContact.name : null
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
      nzTitle: `${ isAdd ? ADD : EDIT } ${ORGANIZATION}`,
      nzContent: ManageOrganizationComponent,
      nzWidth: LARGE_MODAL_WIDTH,
      nzComponentParams: {
        isAdd: isAdd,
        data: d,
        countries: this.countries,
        parentTypes: this.parentTypes
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