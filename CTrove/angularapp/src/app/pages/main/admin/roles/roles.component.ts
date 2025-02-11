import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { BaseMasterListComponent } from 'src/app/pages/common/base-class';
import { ROLES_FIELDS, Role, RolesListFilter } from 'src/app/models/dto/role';
import { RolesService } from 'src/app/services/main/roles.service';
import { CommonService } from 'src/app/services/common/common.service';
import { BLINDED_OPTIONS } from 'src/Utilities/common/app-data';
import { NzModalRef, NzModalService } from 'ng-zorro-antd/modal';
import { ADD, EDIT, ROLES_PAGE } from 'src/Utilities/common/app-strings';
import { ManageRolesComponent } from './manage-roles/manage-roles.component';
import { DEF_MODAL_WIDTH } from 'src/Utilities/common/app-variables';

@Component({
  selector: 'app-roles',
  templateUrl: './roles.component.html',
  styleUrls: ['./roles.component.css'],
})
export class RolesComponent extends BaseMasterListComponent implements OnInit {
  fields = ROLES_FIELDS
  blindedOptions = BLINDED_OPTIONS
  override headers = [
    this.fields.code,
    this.fields.name,
    this.fields.status,
    this.fields.blinded,
    this.fields.pageAccess
  ];

  form = new FormGroup({
    search: new FormControl(),
    status: new FormControl(true),
    blinded: new FormControl(false),
  });

  constructor(private rolesService: RolesService,
    private commonService: CommonService,
    private modalService: NzModalService) {
    super();
  }

  ngOnInit() {
    this.getList(this.meta.page, this.meta.limit);

    this.form
      .get('search')
      ?.valueChanges.subscribe((_) =>
        this.getList(this.meta.page, this.meta.limit)
      );

    this.form
      .get('status')
      ?.valueChanges.subscribe((_) =>
        this.getList(this.meta.page, this.meta.limit)
      );

    this.form
      .get('blinded')
      ?.valueChanges.subscribe((_) =>
        this.getList(this.meta.page, this.meta.limit)
      );
  }

  getList = (page: number = this.meta.page, limit: number = this.meta.limit) => 
    this.rolesService
      .getList(
        page,
        limit,
        new RolesListFilter().deserialize(this.form.getRawValue())
      )
      .subscribe({
        next: (response) => {
          this.meta = response.meta;
          this.data = response.data.map((d) => ({
            id: d.id,
            code: d.code,
            name: d.name,
            status: this.commonService.formatStatus(d.status),
            blinded: this.commonService.formatBlinded(d.blinded),
            pageAccess: this.commonService.formatPageAccess(d.rolesPages)
          }));
          if (this.meta.page <= 0) this.meta.page = 1;
        },
        error: (error) => (this.isLoading = false),
        complete: () => (this.isLoading = false),
      });

  tableChanged = (val: any) => this.getList(val.pageIndex, val.pageSize)
  
  manage(isAdd: boolean, role: Role | null = null) {
    const modal: NzModalRef = this.modalService.create({
      nzTitle: `${ isAdd ? ADD : EDIT } ${ROLES_PAGE}`,
      nzContent: ManageRolesComponent,
      nzWidth: DEF_MODAL_WIDTH,
      nzComponentParams: {
        isAdd: isAdd,
        role: role
      },
      nzFooter: null
    })

    modal.afterClose.subscribe(response => {
      if(response) {
        this.reset()
        this.getList()
      }
    })
  }
}
