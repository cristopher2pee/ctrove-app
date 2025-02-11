import { Injectable } from '@angular/core';
import { HttpService, PHASE_URL } from '../../http.service';
import { Phase, PhaseFilter } from 'src/app/models/dto/phase';
import { PageList } from 'src/app/models/dto/common';

@Injectable({
  providedIn: 'root'
})
export class PhaseService {
  
  constructor(private httpService: HttpService) { }

  getDefList = () => this.httpService.get<PageList<Phase>>(`${PHASE_URL}?status=true`)

  getList = (index: number, size: number, filter: PhaseFilter) => {
    let url = `${PHASE_URL}?page=${index}&limit=${size}&status=${filter.status}`
    if (filter.search) 
      url = `${url}&search=${filter.search}`;
    return this.httpService.get<PageList<Phase>>(url);
  }

  getById = (id: string) => this.httpService.get<Phase>(`${PHASE_URL}/${id}`)

  save = (d: Phase) => this.httpService.post(PHASE_URL, d)

  update = (d: Phase) => this.httpService.put(PHASE_URL, d)

  remove = (d: Phase) => this.httpService.delete(PHASE_URL, d)
}
