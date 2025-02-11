import { Injectable } from '@angular/core';
import { HttpService, STUDY_SITES_URL, STUDY_SITE_PHASE_SURL } from '../http.service';
import { PageList } from 'src/app/models/dto/common';
import { Site, SitePhase, SitesFilter } from 'src/app/models/dto/site';

@Injectable({
  providedIn: 'root'
})
export class SitesService {

  constructor(private httpService: HttpService) { }

  getDefList = () => this.httpService.get<PageList<Site>>(`${STUDY_SITES_URL}?status=true`)

  getList = (index: number, size: number, filter: SitesFilter) => {
    let url = `${STUDY_SITES_URL}?page=${index}&limit=${size}&status=${filter.status}`
    if (filter.search) 
      url = `${url}&search=${filter.search}`;

    if(filter.siteStatusId && filter.siteStatusId.length > 0) 
      filter.siteStatusId.forEach(id => url += `&siteStatusId=${id}`)

    if(filter.studyCountryId && filter.studyCountryId.length > 0) 
      filter.studyCountryId.forEach(id => url += `&studyCountryId=${id}`)

    if(filter.serviceTypeId && filter.serviceTypeId.length > 0) 
      filter.serviceTypeId.forEach(id => url += `&serviceTypeId=${id}`)

    return this.httpService.get<PageList<Site>>(url);
  }
  
  getById = (id: string) => this.httpService.get<Site>(`${STUDY_SITES_URL}/${id}`);

  save = (d: Site) => this.httpService.post(STUDY_SITES_URL, d)

  update = (d: Site) => this.httpService.put(STUDY_SITES_URL, d)

  remove = (d: Site) => this.httpService.put(STUDY_SITES_URL, d)

  getPhaseById = (id: string) => this.httpService.get<SitePhase>(`${STUDY_SITE_PHASE_SURL}/${id}`)
  
  updatePhase = (d: SitePhase) => this.httpService.put(STUDY_SITE_PHASE_SURL, d)

  removePhase = (d: SitePhase) => this.httpService.delete(STUDY_SITE_PHASE_SURL, d)
}
