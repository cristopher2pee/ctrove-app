import { Injectable } from '@angular/core';
import { ETHNICITY_URL, HttpService } from '../../http.service';
import { PageList } from 'src/app/models/dto/common';
import { Ethnicity, EthnicityListFilter } from 'src/app/models/dto/ethnicity';

@Injectable({
  providedIn: 'root',
})
export class EthnicityService {
  constructor(private httpService: HttpService) {}

  getDefList = () =>
    this.httpService.get<PageList<Ethnicity>>(`${ETHNICITY_URL}?status=true`);

  getList = (index: number, size: number, filter: EthnicityListFilter) => {
    let url = `${ETHNICITY_URL}?page=${index}&limit=${size}&status=${filter.status}`;
    if (filter.search) url = `${url}&search=${filter.search}`;
    return this.httpService.get<PageList<Ethnicity>>(url);
  };

  getById = (id: string) =>
    this.httpService.get<Ethnicity>(`${ETHNICITY_URL}/${id}`);

  save = (d: Ethnicity) => this.httpService.post(ETHNICITY_URL, d);

  update = (d: Ethnicity) => this.httpService.put(ETHNICITY_URL, d);

  remove = (d: Ethnicity) => this.httpService.delete(ETHNICITY_URL, d);
}
