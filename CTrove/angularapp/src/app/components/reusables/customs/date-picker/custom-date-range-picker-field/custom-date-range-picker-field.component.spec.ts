import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomDateRangePickerFieldComponent } from './custom-date-range-picker-field.component';

describe('CustomDateRangePickerFieldComponent', () => {
  let component: CustomDateRangePickerFieldComponent;
  let fixture: ComponentFixture<CustomDateRangePickerFieldComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CustomDateRangePickerFieldComponent]
    });
    fixture = TestBed.createComponent(CustomDateRangePickerFieldComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
