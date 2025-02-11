import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot } from '@angular/router';
import { AccountService } from '../services/account.service';
import { RoutingService } from '../services/routing.service';

@Injectable({
  providedIn: 'root'
})
export class RootGuard implements CanActivate {

  constructor(private accountService: AccountService,
    private routingService: RoutingService) { }

  async canActivate(route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Promise<boolean> {
    let hasAccess = this.accountService.isTokenExpired();

    if(!hasAccess)
      this.routingService.toMain()

    return hasAccess;
  }
}