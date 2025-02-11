import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrialClassificationComponent } from './trial-classification.component';

describe('TrialClassificationComponent', () => {
  let component: TrialClassificationComponent;
  let fixture: ComponentFixture<TrialClassificationComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TrialClassificationComponent]
    });
    fixture = TestBed.createComponent(TrialClassificationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
