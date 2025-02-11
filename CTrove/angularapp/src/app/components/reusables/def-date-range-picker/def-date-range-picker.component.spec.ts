import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DefDateRangePickerComponent } from './def-date-range-picker.component';

describe('DefDateRangePickerComponent', () => {
  let component: DefDateRangePickerComponent;
  let fixture: ComponentFixture<DefDateRangePickerComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DefDateRangePickerComponent]
    });
    fixture = TestBed.createComponent(DefDateRangePickerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
