import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageVisitComponent } from './manage-visit.component';

describe('ManageVisitComponent', () => {
  let component: ManageVisitComponent;
  let fixture: ComponentFixture<ManageVisitComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ManageVisitComponent]
    });
    fixture = TestBed.createComponent(ManageVisitComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
