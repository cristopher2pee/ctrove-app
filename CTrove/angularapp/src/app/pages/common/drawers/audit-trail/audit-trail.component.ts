import { Component, OnInit } from '@angular/core';
import { NzDrawerRef } from 'ng-zorro-antd/drawer';
import { ADDED, REMOVED, UPDATED, VIEWED } from 'src/Utilities/common/app-strings';
import { Audit } from 'src/app/models/dto/audit';
import { AuditService } from 'src/app/services/common/audit.service';
import { CommonService } from 'src/app/services/common/common.service';
import { BaseComponent } from '../../base-class';

@Component({
  selector: 'app-audit-trail',
  templateUrl: './audit-trail.component.html',
  styleUrls: ['./audit-trail.component.css']
})
export class AuditTrailComponent extends BaseComponent implements OnInit {
  id!: string
  audits!: Audit[]
  auditTypeTexts = [UPDATED, VIEWED, ADDED, UPDATED, REMOVED]

  constructor(private drawerRef: NzDrawerRef<string>,
    private auditService: AuditService,
    public commonService: CommonService) { 
    super()
    this.drawerRef.nzPlacement = 'right'
  }

  ngOnInit() {
    this.isLoading = true
    this.auditService.getDefList(this.id).subscribe({
      next: (response) => this.audits = response,
      error: () => {
        this.commonService.showErrorMessage()
        this.isLoading = false
      },
      complete: () => this.isLoading = false
    })
  }
}
