import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DefTextareaComponent } from './def-textarea.component';

describe('DefTextareaComponent', () => {
  let component: DefTextareaComponent;
  let fixture: ComponentFixture<DefTextareaComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DefTextareaComponent]
    });
    fixture = TestBed.createComponent(DefTextareaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
