import { Injectable } from '@angular/core';
import { HttpService, RACE_URL } from '../../http.service';
import { PageList } from 'src/app/models/dto/common';
import { Race, RaceListFilter } from 'src/app/models/dto/race';

@Injectable({
  providedIn: 'root'
})
export class RaceService {
  
  constructor(private httpService: HttpService) { }

  getDefList = () => this.httpService.get<PageList<Race>>(`${RACE_URL}?status=true`)

  getList = (index: number, size: number, filter: RaceListFilter) => {
    let url = `${RACE_URL}?page=${index}&limit=${size}&status=${filter.status}`
    if (filter.search) 
      url = `${url}&search=${filter.search}`;
    return this.httpService.get<PageList<Race>>(url);
  }

  getById = (id: string) => this.httpService.get<Race>(`${RACE_URL}/${id}`)

  save = (d: Race) => this.httpService.post(RACE_URL, d)

  update = (d: Race) => this.httpService.put(RACE_URL, d)

  remove = (d: Race) => this.httpService.delete(RACE_URL, d)
}
