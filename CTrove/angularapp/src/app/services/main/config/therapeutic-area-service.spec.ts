import { TestBed } from '@angular/core/testing';

import { TherapeuticAreaService } from './therapeutic-area.service';

describe('TherapeuticAreaServiceService', () => {
  let service: TherapeuticAreaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TherapeuticAreaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
