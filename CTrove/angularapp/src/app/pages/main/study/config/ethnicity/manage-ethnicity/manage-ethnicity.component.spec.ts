import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageEthnicityComponent } from './manage-ethnicity.component';

describe('ManageEthnicityComponent', () => {
  let component: ManageEthnicityComponent;
  let fixture: ComponentFixture<ManageEthnicityComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ManageEthnicityComponent]
    });
    fixture = TestBed.createComponent(ManageEthnicityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
