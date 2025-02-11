import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomSelectFieldComponent } from './custom-select-field.component';

describe('CustomSelectFieldComponent', () => {
  let component: CustomSelectFieldComponent;
  let fixture: ComponentFixture<CustomSelectFieldComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CustomSelectFieldComponent]
    });
    fixture = TestBed.createComponent(CustomSelectFieldComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
