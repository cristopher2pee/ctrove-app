import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DefDatePickerComponent } from './def-date-picker.component';

describe('DefDatePickerComponent', () => {
  let component: DefDatePickerComponent;
  let fixture: ComponentFixture<DefDatePickerComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DefDatePickerComponent]
    });
    fixture = TestBed.createComponent(DefDatePickerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
