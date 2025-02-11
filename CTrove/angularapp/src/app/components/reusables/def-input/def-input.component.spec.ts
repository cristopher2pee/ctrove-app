import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DefInputComponent } from './def-input.component';

describe('DefInputComponent', () => {
  let component: DefInputComponent;
  let fixture: ComponentFixture<DefInputComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DefInputComponent]
    });
    fixture = TestBed.createComponent(DefInputComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
