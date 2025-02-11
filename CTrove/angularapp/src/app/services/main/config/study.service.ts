import { Injectable } from '@angular/core';
import { HttpService, STUDY_URL } from '../../http.service';
import { PageList } from 'src/app/models/dto/common';
import { Study } from 'src/app/models/dto/study';

@Injectable({
  providedIn: 'root'
})
export class StudyService {

  constructor(private httpService: HttpService) { }

  getDefList = () => this.httpService.get<PageList<Study>>(`${STUDY_URL}?status=true`)

  save = (d: Study) => this.httpService.post(STUDY_URL, d)

  update = (d: Study) => this.httpService.put(STUDY_URL, d)
}
