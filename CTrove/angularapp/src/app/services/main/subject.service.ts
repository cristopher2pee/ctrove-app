import { Injectable } from '@angular/core';
import { HttpService, SUBJECT_PHASE_URL, SUBJECT_URL } from '../http.service';
import { PageList } from 'src/app/models/dto/common';
import { Subject, SubjectFilter, SubjectPhase } from 'src/app/models/dto/subject';

@Injectable({
  providedIn: 'root'
})
export class SubjectService {

  constructor(private httpService: HttpService) { }
  
  getDefList = () => this.httpService.get<PageList<Subject>>(`${SUBJECT_URL}?status=true`)

  getList = (index: number, size: number, filter: SubjectFilter) => {
    let url = `${SUBJECT_URL}?page=${index}&limit=${size}&status=${filter.status}`
    if (filter.search) 
      url = `${url}&search=${filter.search}`;

    if(filter.studyCountryIds && filter.studyCountryIds.length > 0) 
      filter.studyCountryIds.forEach(id => url += `&studyCountryIds=${id}`)

    if(filter.sitesIds && filter.sitesIds.length > 0) 
      filter.sitesIds.forEach(id => url += `&sitesIds=${id}`)

    if(filter.subjectStatus && filter.subjectStatus.length > 0) 
      filter.subjectStatus.forEach(id => url += `&subjectStatus=${id}`)

    return this.httpService.get<PageList<Subject>>(url);
  }

  getById = (id: string) => this.httpService.get<Subject>(`${SUBJECT_URL}/${id}`)

  save = (d: Subject) => this.httpService.post(SUBJECT_URL, d)

  update = (d: Subject) => this.httpService.put(SUBJECT_URL, d)

  remove = (d: Subject) => this.httpService.delete(SUBJECT_URL, d)

  getPhaseById = (id: string) => this.httpService.get<SubjectPhase>(`${SUBJECT_PHASE_URL}/${id}`)
  
  updatePhase = (d: SubjectPhase) => this.httpService.put(SUBJECT_PHASE_URL, d)

  removePhase = (d: SubjectPhase) => this.httpService.delete(SUBJECT_PHASE_URL, d)
}

