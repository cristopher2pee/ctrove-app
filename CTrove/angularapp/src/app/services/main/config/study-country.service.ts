import { Injectable } from '@angular/core';
import { PageList } from 'src/app/models/dto/common';
import { StudyCountry, StudyCountryFilter } from 'src/app/models/dto/study-country';
import { HttpService, STUDY_COUNTRY_URL } from '../../http.service';

@Injectable({
  providedIn: 'root'
})
export class StudyCountryService {

  constructor(private httpService: HttpService) { }

  getDefList = () => this.httpService.get<PageList<StudyCountry>>(`${STUDY_COUNTRY_URL}?status=true`)

  getList = (index: number, size: number, filter: StudyCountryFilter) => {
    let url = `${STUDY_COUNTRY_URL}?page=${index}&limit=${size}&status=${filter.status}`
    if (filter.search) 
      url = `${url}&search=${filter.search}`;
    return this.httpService.get<PageList<StudyCountry>>(url);
  }

  getById = (id: string) => this.httpService.get<StudyCountry>(`${STUDY_COUNTRY_URL}/${id}`)

  save = (d: StudyCountry) => this.httpService.post(STUDY_COUNTRY_URL, d)

  update = (d: StudyCountry) => this.httpService.put(STUDY_COUNTRY_URL, d)

  remove = (d: StudyCountry) => this.httpService.delete(STUDY_COUNTRY_URL, d)
}
