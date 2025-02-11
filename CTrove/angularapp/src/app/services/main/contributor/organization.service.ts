import { Injectable } from '@angular/core';
import { HttpService, ORGANIZATION_EXIST_URL, ORGANIZATION_URL } from '../../http.service';
import {
  Organization,
  OrganizationFilter,
} from 'src/app/models/contributor/organization';
import { PageList } from 'src/app/models/dto/common';

@Injectable({
  providedIn: 'root',
})
export class OrganizationService {
  constructor(private httpService: HttpService) {}

  getListResources = () =>
    this.httpService.get<PageList<Organization>>(
      `${ORGANIZATION_URL}?status=true`
    )

  getList = (index: number, size: number, filter: OrganizationFilter) => {
    let url = `${ORGANIZATION_URL}?page=${index}&limit=${size}&status=${filter.status}`

    if (filter.search) url = `${url}&search=${filter.search}`

    if (filter.countrysId && filter.countrysId.length > 0)
      filter.countrysId.forEach((id) => (url += `&listCountryId=${id}`))

    if (filter.parentTypes && filter.parentTypes.length > 0)
      filter.parentTypes.forEach((id) => (url += `&parentTypes=${id}`))

    return this.httpService.get<PageList<Organization>>(url)
  };

  getById = (id: string) =>
    this.httpService.get<Organization>(`${ORGANIZATION_URL}/${id}`)

  save = (entity: Organization) =>
    this.httpService.post(ORGANIZATION_URL, entity)

  update = (entity: Organization) =>
    this.httpService.put(ORGANIZATION_URL, entity)

  remove = (entity: Organization) =>
    this.httpService.delete(ORGANIZATION_URL, entity)

  checkIfCompanyExist = (name: string) =>
    this.httpService.get<any>(`${ORGANIZATION_EXIST_URL}?parameter=${name}`)

}
