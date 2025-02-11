import { TestBed } from '@angular/core/testing';

import { StudySitesService } from './study-sites.service';

describe('StudySitesService', () => {
  let service: StudySitesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(StudySitesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
