import { Injectable } from '@angular/core';
import { PageList } from 'src/app/models/dto/common';
import { TAListFilter, TherapeuticArea } from 'src/app/models/dto/therapeutic-area';
import { HttpService, THERAPEUTIC_AREA_URL } from '../../http.service';

@Injectable({
  providedIn: 'root'
})
export class TherapeuticAreaService {

  constructor(private httpService: HttpService) { }

  getList = (index: number, size: number, filter: TAListFilter) => {
    let url = `${THERAPEUTIC_AREA_URL}?page=${index}&limit=${size}&status=${filter.status}`
    if (filter.search) 
      url = `${url}&search=${filter.search}`;
    return this.httpService.get<PageList<TherapeuticArea>>(url);
  }

  getById = (id: string) => this.httpService.get<TherapeuticArea>(`${THERAPEUTIC_AREA_URL}/${id}`)

  save = (d: TherapeuticArea) => this.httpService.post(THERAPEUTIC_AREA_URL, d)

  update = (d: TherapeuticArea) => this.httpService.put(THERAPEUTIC_AREA_URL, d)

  remove = (d: any) => this.httpService.delete(`${THERAPEUTIC_AREA_URL}/${d.id}`, d)
}
