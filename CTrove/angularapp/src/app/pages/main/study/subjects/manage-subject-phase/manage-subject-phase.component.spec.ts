import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageSubjectPhaseComponent } from './manage-subject-phase.component';

describe('ManageSubjectPhaseComponent', () => {
  let component: ManageSubjectPhaseComponent;
  let fixture: ComponentFixture<ManageSubjectPhaseComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ManageSubjectPhaseComponent]
    });
    fixture = TestBed.createComponent(ManageSubjectPhaseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
