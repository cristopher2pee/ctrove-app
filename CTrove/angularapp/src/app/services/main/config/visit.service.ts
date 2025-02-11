import { Injectable } from '@angular/core';
import { HttpService, VISIT_URL } from '../../http.service';
import { PageList } from 'src/app/models/dto/common';
import { Visit, VisitFilter } from 'src/app/models/dto/visit';

@Injectable({
  providedIn: 'root'
})
export class VisitService {

  constructor(private httpService: HttpService) { }

  getDefList = () => this.httpService.get<PageList<Visit>>(`${VISIT_URL}?status=true`)

  getList = (index: number, size: number, filter: VisitFilter) => {
    let url = `${VISIT_URL}?page=${index}&limit=${size}&status=${filter.status}`
    if (filter.search) 
      url = `${url}&search=${filter.search}`;
    return this.httpService.get<PageList<Visit>>(url);
  }

  getById = (id: string) => this.httpService.get<Visit>(`${VISIT_URL}/${id}`)

  save = (d: Visit) => this.httpService.post(VISIT_URL, d)

  update = (d: Visit) => this.httpService.put(VISIT_URL, d)

  remove = (d: Visit) => this.httpService.delete(VISIT_URL, d)
}
