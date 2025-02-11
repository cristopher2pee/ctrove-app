import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DefCheckboxComponent } from './def-checkbox.component';

describe('DefCheckboxComponent', () => {
  let component: DefCheckboxComponent;
  let fixture: ComponentFixture<DefCheckboxComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DefCheckboxComponent]
    });
    fixture = TestBed.createComponent(DefCheckboxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
