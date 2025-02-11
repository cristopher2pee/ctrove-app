import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SubNavigationService {
  private subPath = new BehaviorSubject('')

  currentSubPath = this.subPath.asObservable()

  constructor() { }

  updateSubPath = (path: string) => this.subPath.next(path)
}
