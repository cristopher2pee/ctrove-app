import { Injectable } from '@angular/core';
import { TCFilter, TrialClassification } from 'src/app/models/dto/trial-classification';
import { PageList } from 'src/app/models/dto/common';
import { HttpService, TRIAL_CLASSIFICATION_URL } from '../../http.service';

@Injectable({
  providedIn: 'root'
})
export class TrialClassificationService {

  constructor(private httpService: HttpService) { }

  getList = (index: number, size: number, filter: TCFilter) => {
    let url = `${TRIAL_CLASSIFICATION_URL}?page=${index}&limit=${size}&status=${filter.status}`
    if (filter.search) 
      url = `${url}&search=${filter.search}`;
    return this.httpService.get<PageList<TrialClassification>>(url);
  }

  getById = (id: string) => this.httpService.get<TrialClassification>(`${TRIAL_CLASSIFICATION_URL}/${id}`)

  save = (d: TrialClassification) => this.httpService.post(TRIAL_CLASSIFICATION_URL, d)

  update = (d: TrialClassification) => this.httpService.put(TRIAL_CLASSIFICATION_URL, d)

  remove = (d: TrialClassification) => this.httpService.delete(TRIAL_CLASSIFICATION_URL, d)
}
