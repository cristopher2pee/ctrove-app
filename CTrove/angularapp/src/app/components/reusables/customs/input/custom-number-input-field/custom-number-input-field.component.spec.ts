import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomNumberInputFieldComponent } from './custom-number-input-field.component';

describe('CustomNumberInputFieldComponent', () => {
  let component: CustomNumberInputFieldComponent;
  let fixture: ComponentFixture<CustomNumberInputFieldComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CustomNumberInputFieldComponent]
    });
    fixture = TestBed.createComponent(CustomNumberInputFieldComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
