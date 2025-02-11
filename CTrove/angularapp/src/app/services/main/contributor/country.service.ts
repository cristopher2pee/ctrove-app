import { Injectable } from '@angular/core';
import { HttpService, COUNTRY_URL } from '../../http.service';
import { PageList } from 'src/app/models/dto/common';
import { Country, CountryFilter } from 'src/app/models/contributor/country';

@Injectable({
  providedIn: 'root',
})
export class CountryService {
  constructor(private httpService: HttpService) {}

  getListResources = () =>
    this.httpService.get<PageList<Country>>(`${COUNTRY_URL}?status=true&limit=200`);

  getList = () => (index: number, size: number, filter: CountryFilter) => {
    let url = `${COUNTRY_URL}?page=${index}&limit=${size}&status=${filter.status}`;
    if (filter.search) url = `${url}&search=${filter.search}`;
    return this.httpService.get<PageList<Country>>(url);
  };

  getById = (id: string) =>
    this.httpService.get<Country>(`${COUNTRY_URL}/${id}`);

  save = (entity: Country) => this.httpService.post(COUNTRY_URL, entity);

  update = (entity: Country) => this.httpService.put(COUNTRY_URL, entity);

  remove = (entity: Country) => this.httpService.delete(COUNTRY_URL, entity);
}
