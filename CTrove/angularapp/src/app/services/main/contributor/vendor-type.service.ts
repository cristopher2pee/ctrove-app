import { Injectable } from '@angular/core';
import { HttpService, VENDOR_TYPE_URL } from '../../http.service';
import { PageList } from 'src/app/models/dto/common';
import {
  VendorType,
  VendorTypeFilter,
} from 'src/app/models/contributor/vendor-type';

@Injectable({
  providedIn: 'root',
})
export class VendorTypeService {
  constructor(private httpService: HttpService) {}

  getListResources = () =>
    this.httpService.get<PageList<VendorType>>(
      `${VENDOR_TYPE_URL}?status=true`
    );

  getListPage =
    () => (index: number, size: number, filter: VendorTypeFilter) => {
      let url = `${VENDOR_TYPE_URL}?page=${index}&limit=${size}&status=${filter.status}`;
      if (filter.search) url = `${url}&search=${filter.search}`;
      return this.httpService.get<PageList<VendorType>>(url);
    };

  getById = (id: string) =>
    this.httpService.get<VendorType>(`${VENDOR_TYPE_URL}/${id}`);

  save = (entity: VendorType) => this.httpService.post(VENDOR_TYPE_URL, entity);

  update = (entity: VendorType) =>
    this.httpService.put(VENDOR_TYPE_URL, entity);

  remove = (entity: VendorType) =>
    this.httpService.delete(VENDOR_TYPE_URL, entity);
}
