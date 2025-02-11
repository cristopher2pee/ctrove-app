import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class RoutingService {

  constructor(private router: Router) { }

  private navigate = (path: string) => this.router.navigate([path])

  to = (path: string) => this.navigate(path)

  toRoot = () => this.navigate('login')
  
  toMain = () => this.navigate('main')

  toProfile = () => this.navigate('../main/profile')

  toOnboarding = () => this.navigate('onboarding')
}
