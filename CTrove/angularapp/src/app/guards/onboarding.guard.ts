import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot } from '@angular/router';
import { AccountService } from '../services/account.service';
import { firstValueFrom } from 'rxjs';
import { RoutingService } from '../services/routing.service';

@Injectable({
  providedIn: 'root'
})
export class OnboardingGuard implements CanActivate {

  constructor(private accountService: AccountService, 
    private routingService: RoutingService) { }

  async canActivate(route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Promise<boolean> {
    let hasAccess = await firstValueFrom(this.accountService.isOnboarding())

    if(!hasAccess)
      this.routingService.toOnboarding()
      
    return hasAccess
  }
}