import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { NzDrawerService } from 'ng-zorro-antd/drawer';
import { AUDIT_TRAIL_PAGE } from 'src/Utilities/common/app-strings';
import { BIN_FIELDS } from 'src/app/models/dto/common';
import { BaseComponent } from 'src/app/pages/common/base-class';
import { AuditTrailComponent } from 'src/app/pages/common/drawers/audit-trail/audit-trail.component';

@Component({
  selector: 'app-custom-modal-footer',
  templateUrl: './custom-modal-footer.component.html',
  styleUrls: ['./custom-modal-footer.component.css']
})
export class CustomModalFooterComponent extends BaseComponent {
  @Output() statusChanged = new EventEmitter
  @Output() submit = new EventEmitter()
  @Input() remarksControl!: FormControl
  @Input() statusControl!: FormControl
  @Input() id!: string | null | undefined
  @Input() override isLoading: boolean = false
  @Input() isTouched: boolean = false
  @Input() isAdd: boolean = true
  fields = BIN_FIELDS

  constructor(private drawerService: NzDrawerService) {
    super();
  }

  openAudit() {
    if(!this.id)
      return

    this.drawerService.create<AuditTrailComponent, { value: string }, string>({
      nzContent: AuditTrailComponent,
      nzTitle: AUDIT_TRAIL_PAGE,
      nzContentParams: {
        id: this.id
      }
    })
  }
}
