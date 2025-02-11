import { Injectable } from '@angular/core';
import { AUDIT_LIST_URL, HttpService } from '../http.service';

@Injectable({
  providedIn: 'root'
})
export class AuditTrailService {

  constructor(private httpService: HttpService) { }

  getDefList = (id: string) => this.httpService.get(AUDIT_LIST_URL.replace('id', id))
}
