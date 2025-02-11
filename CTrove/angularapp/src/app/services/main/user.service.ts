import { Injectable } from '@angular/core';
import {
  HttpService,
  INVITE_EMAIL_URL,
  INVITE_USER_URL,
  USER_ACCESS_RIGHTS_URL,
  USER_EMAIL_EXIST_URL,
  USER_URL,
} from '../http.service';
import { User, UserListFilter } from 'src/app/models/dto/user';
import { PageList } from 'src/app/models/dto/common';
import { CustomAccess } from 'src/app/models/dto/access';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private httpService: HttpService) {}

  getDefList = () =>
    this.httpService.get<PageList<User>>(`${USER_URL}?status=true`);

  getList = (index: number, size: number, filter: UserListFilter) => {
    let url = `${USER_URL}?page=${index}&limit=${size}&status=${filter.status}`;

    if (filter.search) url = `${url}&search=${filter.search}`;

    if (filter.rolesId && filter.rolesId.length > 0)
      filter.rolesId.forEach((id) => (url += `&rolesId=${id}`));

    return this.httpService.get<PageList<User>>(url);
  };

  getAccessList = (userId: string) =>
    this.httpService.get<CustomAccess[]>(
      USER_ACCESS_RIGHTS_URL.replace('id', userId)
    );

  getById = (id: string) => this.httpService.get<User>(`${USER_URL}/${id}`);

  validateEmail = (email: string) =>
    this.httpService.get<boolean>(`${USER_EMAIL_EXIST_URL}?email=${email}`);

  save = (user: User) => this.httpService.post<User>(USER_URL, user);

  update = (user: User) => this.httpService.put<User>(USER_URL, user);

  invite = (user: User) => this.httpService.post(INVITE_USER_URL, user);

  inviteEmail = (user: User) => this.httpService.post(INVITE_EMAIL_URL, user);

  remove = (user: User) => this.httpService.delete<User>(USER_URL, user);
}
