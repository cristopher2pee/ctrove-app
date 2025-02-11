import { Injectable } from '@angular/core';
import { HttpService, ROLES_RESOURCE_URL } from '../http.service';

@Injectable({
  providedIn: 'root'
})
export class ResourceService {

  constructor(private httpService: HttpService) { }

  getInviteResource = () => this.httpService.get(ROLES_RESOURCE_URL)
}
