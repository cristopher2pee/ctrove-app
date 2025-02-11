import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MessagingService {
  userSite = new Subject()

  constructor() { }

  userSiteOnChange = () => this.userSite.next(null)

  userSiteChanged = () => this.userSite.asObservable()
}
