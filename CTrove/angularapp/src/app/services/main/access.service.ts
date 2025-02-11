import { Injectable } from '@angular/core';
import { ACCESS_IS_EXISTING_URL, ACCESS_LIST_URL, ACCESS_URL, HttpService } from '../http.service';
import { Access, AccessListFilter, CustomAccess } from 'src/app/models/dto/access';
import { PageList } from 'src/app/models/dto/common';

@Injectable({
  providedIn: 'root'
})
export class AccessService {

  constructor(private httpService: HttpService) { }

  getList = (index: number, size: number, filter: AccessListFilter) => {
    let url = `${ACCESS_LIST_URL}?page=${index}&limit=${size}&status=${filter.status}&right=${filter.right}`

    if(filter.search)
      url = `${url}&search=${filter.search}`

    return this.httpService.get<PageList<CustomAccess>>(url)
  } 

  getById = (id: string) => this.httpService.get<CustomAccess>(`${ACCESS_URL}/${id}`);

  isExisting = (access: Access) => this.httpService.get(`${ACCESS_IS_EXISTING_URL}?right=${access.rights}&accessLevelId=${access.accessLevelId}&userId=${access.userId}`)

  save = (access: Access) => this.httpService.post(ACCESS_URL, access)

  update = (access: Access) => this.httpService.put(ACCESS_URL, access)

  remove = (access: Access) => this.httpService.delete(ACCESS_URL, access)
}
