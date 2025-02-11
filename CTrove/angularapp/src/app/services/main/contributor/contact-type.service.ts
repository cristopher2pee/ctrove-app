import { Injectable } from '@angular/core';
import { HttpService, CONTACT_TYPE_URL } from '../../http.service';
import { PageList } from 'src/app/models/dto/common';
import {
  ContactType,
  ContactTypeFilter,
} from 'src/app/models/contributor/contact-type';

@Injectable({
  providedIn: 'root',
})
export class ContactTypeService {
  constructor(private httpService: HttpService) {}

  getListResources = () =>
    this.httpService.get<PageList<ContactType>>(
      `${CONTACT_TYPE_URL}?status=true`
    );

  getListPage =
    () => (index: number, size: number, filter: ContactTypeFilter) => {
      let url = `${CONTACT_TYPE_URL}?page=${index}&limit=${size}&status=${filter.status}`;
      if (filter.search) url = `${url}&search=${filter.search}`;
      return this.httpService.get<PageList<ContactType>>(url);
    };

  getById = (id: string) =>
    this.httpService.get<ContactType>(`${CONTACT_TYPE_URL}/${id}`);

  save = (entity: ContactType) =>
    this.httpService.post(CONTACT_TYPE_URL, entity);

  update = (entity: ContactType) =>
    this.httpService.put(CONTACT_TYPE_URL, entity);

  remove = (entity: ContactType) =>
    this.httpService.delete(CONTACT_TYPE_URL, entity);
}
