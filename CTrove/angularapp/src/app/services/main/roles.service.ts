import { Injectable } from '@angular/core';
import { HttpService, ROLES_PAGES_URL, ROLES_URL } from '../http.service';
import { PageList } from 'src/app/models/dto/common';
import { Role, RolesListFilter } from 'src/app/models/dto/role';
@Injectable({
  providedIn: 'root',
})
export class RolesService {
  constructor(private httpService: HttpService) {}

  getList = (index: number, size: number, filter: RolesListFilter) => {
    let url = `${ROLES_URL}?page=${index}&limit=${size}&status=${filter.status}&isBlinded=${filter.blinded}`;
    if (filter.search) url = `${url}&search=${filter.search}`;
    return this.httpService.get<PageList<Role>>(url);
  };

  getById = (id: string) => this.httpService.get<Role>(`${ROLES_URL}/${id}`);

  save = (role: Role) => this.httpService.post(ROLES_URL, role);

  update = (role: Role) => this.httpService.put(ROLES_URL, role);

  remove = (role: Role) => this.httpService.put(ROLES_PAGES_URL, role);
}
