import { Component, Input } from '@angular/core';
import { NzModalService } from 'ng-zorro-antd/modal';
import { LOGOUT_CONFIRM, LOGOUT_CONFIRM_DESC } from 'src/Utilities/common/app-strings';
import { Site } from 'src/app/models/dto/site';
import { User } from 'src/app/models/dto/user';
import { AccountService } from 'src/app/services/account.service';
import { CommonService } from 'src/app/services/common/common.service';
import { RoutingService } from 'src/app/services/routing.service';

@Component({
  selector: 'app-def-user-profile',
  templateUrl: './def-user-profile.component.html',
  styleUrls: ['./def-user-profile.component.css']
})
export class DefUserProfileComponent {
  @Input() user!: User
  @Input() sites!: Site[]

  constructor(private modal: NzModalService,
    private accountService: AccountService,
    private routingService: RoutingService,
    public commonService: CommonService) {
    
  }

  profile = () => this.routingService.toProfile()

  logout = () => this.modal.confirm({
    nzTitle: LOGOUT_CONFIRM,
    nzContent: LOGOUT_CONFIRM_DESC,
    nzOnOk: () => this.accountService.unathenticate()
  })
}
