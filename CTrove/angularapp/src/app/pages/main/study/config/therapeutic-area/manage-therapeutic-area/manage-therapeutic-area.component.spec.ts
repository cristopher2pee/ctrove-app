import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageTherapeuticAreaComponent } from './manage-therapeutic-area.component';

describe('ManageTherapeuticAreaComponent', () => {
  let component: ManageTherapeuticAreaComponent;
  let fixture: ComponentFixture<ManageTherapeuticAreaComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ManageTherapeuticAreaComponent]
    });
    fixture = TestBed.createComponent(ManageTherapeuticAreaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
