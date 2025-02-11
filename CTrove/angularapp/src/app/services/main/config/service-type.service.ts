import { Injectable } from '@angular/core';
import { ServiceType, ServiceTypeFilter } from 'src/app/models/dto/service-type';
import { PageList } from 'src/app/models/dto/common';
import { HttpService, SERVICE_TYPE_URL } from '../../http.service';

@Injectable({
  providedIn: 'root'
})
export class ServiceTypeService {

  constructor(private httpService: HttpService) { }

  getDefList = () => this.httpService.get<PageList<ServiceType>>(`${SERVICE_TYPE_URL}?status=true`)

  getList = (index: number, size: number, filter: ServiceTypeFilter) => {
    let url = `${SERVICE_TYPE_URL}?page=${index}&limit=${size}&status=${filter.status}`
    if (filter.search) 
      url = `${url}&search=${filter.search}`;
    return this.httpService.get<PageList<ServiceType>>(url);
  }

  getById = (id: string) => this.httpService.get<ServiceType>(`${SERVICE_TYPE_URL}/${id}`)

  save = (d: ServiceType) => this.httpService.post(SERVICE_TYPE_URL, d)

  update = (d: ServiceType) => this.httpService.put(SERVICE_TYPE_URL, d)

  remove = (d: ServiceType) => this.httpService.delete(SERVICE_TYPE_URL, d)
}
