import { Injectable } from '@angular/core';
import { PageList } from 'src/app/models/dto/common';
import { StudySite } from 'src/app/models/dto/study-site';
import { HttpService, STUDY_SITES_URL } from '../../http.service';

@Injectable({
  providedIn: 'root'
})
export class StudySitesService {

  constructor(private httpService: HttpService) { }

  getDefList = (id: string) => this.httpService.get<PageList<StudySite>>(`${STUDY_SITES_URL}?studyCountryId=${id}&status=true`)
}
