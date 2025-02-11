import { Injectable } from '@angular/core';
import { MsalService } from '@azure/msal-angular';
import { AuthenticationResult } from '@azure/msal-browser';
import { AUTH_REQUEST } from 'src/Utilities/common/keys/ms-auth';
import { JwtHelperService } from '@auth0/angular-jwt';
import { LocalStorageService } from './local-storage.service';
import { ACCESS_TOKEN_KEY, USER_PROFILE_KEY, USER_SITE } from 'src/Utilities/common/app-local-storage-keys';
import { RoutingService } from './routing.service';
import { ACCOUNTS_IS_ONBOARDING, ACCOUNTS_ONBOARDING, ACCOUNTS_PROFILE_URL, HttpService } from './http.service';
import { AccountProfile, User } from '../models/dto/user';
import { firstValueFrom } from 'rxjs';
import { Site } from '../models/dto/site';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private msService: MsalService,
    private jwtHelper: JwtHelperService,
    private localStorageService: LocalStorageService,
    private routingService: RoutingService,
    private httpService: HttpService) {
  }

  authenticate = (service: MsalService) => service.loginPopup(AUTH_REQUEST)

  unathenticate = () => this.msService.logoutPopup().subscribe(_ => {
    this.clearAllData()
    this.routingService.toRoot()
  })

  setActiveAccount = (authResult: AuthenticationResult) => {
    try {
      this.msService.instance.setActiveAccount(authResult.account)
      this.saveToken(authResult.accessToken)
      return true
    }
    catch {
      return false
    }
  }

  saveToken = (val: string) => this.localStorageService.save(ACCESS_TOKEN_KEY, val)

  getToken = () => this.localStorageService.get(ACCESS_TOKEN_KEY)

  isTokenExpired = () => this.jwtHelper.isTokenExpired(this.getToken())

  clearUserData = () => this.localStorageService.remove(ACCESS_TOKEN_KEY)

  clearAllData = () => this.localStorageService.clearAll()

  isOnboarding = () => this.httpService.get<boolean>(ACCOUNTS_IS_ONBOARDING) 

  onBoard = (user: User) => this.httpService.post<User>(ACCOUNTS_ONBOARDING, user) 

  getProfile = () => this.httpService.get<AccountProfile>(ACCOUNTS_PROFILE_URL)

  async getUserProfile(shouldSave: boolean = true) {
    // var savedProfile = this.localStorageService.getData(USER_PROFILE_KEY)
    
    // if(savedProfile)
    //   return savedProfile

    let response = await firstValueFrom(this.getProfile())

    if(shouldSave && response)
      this.localStorageService.saveData(USER_PROFILE_KEY, response)

    return response
  }

  setUserSite = (site: Site) => this.localStorageService.saveData(USER_SITE, site)

  getUserSite = () => this.localStorageService.getData(USER_SITE)

  async checkUserSites() {
    let response = await this.getUserProfile()

    if(response) {
      
    }
  }
}


