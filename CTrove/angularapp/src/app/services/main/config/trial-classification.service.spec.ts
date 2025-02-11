import { TestBed } from '@angular/core/testing';

import { TrialClassificationService } from './trial-classification.service';

describe('TrialClassificationService', () => {
  let service: TrialClassificationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TrialClassificationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
