import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageStudyCountryComponent } from './manage-study-country.component';

describe('ManageStudyCountryComponent', () => {
  let component: ManageStudyCountryComponent;
  let fixture: ComponentFixture<ManageStudyCountryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ManageStudyCountryComponent]
    });
    fixture = TestBed.createComponent(ManageStudyCountryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
