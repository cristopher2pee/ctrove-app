import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageTrialClassificationComponent } from './manage-trial-classification.component';

describe('ManageTrialClassificationComponent', () => {
  let component: ManageTrialClassificationComponent;
  let fixture: ComponentFixture<ManageTrialClassificationComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ManageTrialClassificationComponent]
    });
    fixture = TestBed.createComponent(ManageTrialClassificationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
