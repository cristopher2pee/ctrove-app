import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomSelectStatusComponent } from './custom-select-status.component';

describe('CustomSelectStatusComponent', () => {
  let component: CustomSelectStatusComponent;
  let fixture: ComponentFixture<CustomSelectStatusComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CustomSelectStatusComponent]
    });
    fixture = TestBed.createComponent(CustomSelectStatusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
