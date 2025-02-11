import { Component, OnInit } from '@angular/core';
import { MsalService } from '@azure/msal-angular';
import { AuthenticationResult, PublicClientApplication } from '@azure/msal-browser';
import { AccountService } from 'src/app/services/account.service';
import { BaseComponent } from '../common/base-class';
import { RoutingService } from 'src/app/services/routing.service';
import { SIGNIN } from 'src/Utilities/common/app-strings';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent extends BaseComponent implements OnInit {
  images = [
    '../../../assets/images/common/image_1.jpg',
    '../../../assets/images/common/image_2.jpg',
    '../../../assets/images/common/image_3.jpg'
  ]
  loginButton = SIGNIN

  constructor(private accountService: AccountService, 
    private msService: MsalService,
    private routingService: RoutingService) {
    super()
  }

  ngOnInit(): void {
    let msalInstance: PublicClientApplication = this.msService.instance as PublicClientApplication;
    msalInstance["browserStorage"].clear();
  }

  authenticate() {
    this.isLoading = true
    this.accountService.authenticate(this.msService)
      .subscribe({
        next: (response: AuthenticationResult) => {
          if(response.account && this.accountService.setActiveAccount(response)) 
                this.routingService.toMain()
        },
        error: (err) => this.isLoading = false,
        complete: () => this.isLoading = false
      })
  }
}
