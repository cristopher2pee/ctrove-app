import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageServiceTypeComponent } from './manage-service-type.component';

describe('ManageServiceTypeComponent', () => {
  let component: ManageServiceTypeComponent;
  let fixture: ComponentFixture<ManageServiceTypeComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ManageServiceTypeComponent]
    });
    fixture = TestBed.createComponent(ManageServiceTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
