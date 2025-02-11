import { Injectable } from '@angular/core';
import { HttpService, CONTRIBUTOR_URL, CONTRIBUTOR_NAME_EXIST_URL, CONTRIBUTOR_EMAIL_EXIST_URL, CONTRIBUTOR_SEARCH_URL } from '../../http.service';
import { Contributor, ContributorFilter } from 'src/app/models/contributor/contributor';
import { PageList } from 'src/app/models/dto/common';

@Injectable({
  providedIn: 'root',
})
export class ContributorService {
  constructor(private httpService: HttpService) {}

  getListResources = () =>
    this.httpService.get<PageList<Contributor>>(
      `${CONTRIBUTOR_URL}?status=true`
    );

  getList = (index: number, size: number, filter: ContributorFilter) => {
    let url = `${CONTRIBUTOR_URL}?page=${index}&limit=${size}&status=${filter.status}`;
    if (filter.search) url = `${url}&search=${filter.search}`;

    if (filter.countrysId && filter.countrysId.length > 0) 
      filter.countrysId.forEach((id) => (url += `&listCountryId=${id}`))
    
    if (filter.organizationsId && filter.organizationsId.length > 0) 
      filter.organizationsId.forEach(
        (id) => (url += `&listOrganizationId=${id}`)
      )

    return this.httpService.get<PageList<Contributor>>(url);
  };

  getById = (id: string) =>
    this.httpService.get<Contributor>(`${CONTRIBUTOR_URL}/${id}`);

  save = (entity: Contributor) =>
    this.httpService.post(CONTRIBUTOR_URL, entity);

  update = (entity: Contributor) =>
    this.httpService.put(CONTRIBUTOR_URL, entity);

  remove = (entity: Contributor) =>
    this.httpService.delete(CONTRIBUTOR_URL, entity);

  checkIfNameExist = (fn: string, ln: string) => 
    this.httpService.get<any>(`${CONTRIBUTOR_NAME_EXIST_URL}?firstname=${fn}&lastname=${ln}`);

  checkIfEmailExist = (email: string) => 
    this.httpService.get<any>(`${CONTRIBUTOR_EMAIL_EXIST_URL}?email=${email}`);

  search = (name: string) => 
    this.httpService.get<Contributor[]>(`${CONTRIBUTOR_SEARCH_URL}?searchParam=${name}`);
}
