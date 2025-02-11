import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DefNumberInputGroupComponent } from './def-number-input-group.component';

describe('DefNumberInputGroupComponent', () => {
  let component: DefNumberInputGroupComponent;
  let fixture: ComponentFixture<DefNumberInputGroupComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DefNumberInputGroupComponent]
    });
    fixture = TestBed.createComponent(DefNumberInputGroupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
