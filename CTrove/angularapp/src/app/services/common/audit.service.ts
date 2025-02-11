import { Injectable } from '@angular/core';
import { AUDIT_LIST_URL, HttpService } from '../http.service';
import { Audit } from 'src/app/models/dto/audit';

@Injectable({
  providedIn: 'root'
})
export class AuditService {

  constructor(private httpService: HttpService) { }

  getDefList = (id: string) => this.httpService.get<Audit[]>(AUDIT_LIST_URL.replace('id', id))
}
