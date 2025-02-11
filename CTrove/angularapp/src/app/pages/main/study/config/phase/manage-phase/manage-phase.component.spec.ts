import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManagePhaseComponent } from './manage-phase.component';

describe('ManagePhaseComponent', () => {
  let component: ManagePhaseComponent;
  let fixture: ComponentFixture<ManagePhaseComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ManagePhaseComponent]
    });
    fixture = TestBed.createComponent(ManagePhaseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
