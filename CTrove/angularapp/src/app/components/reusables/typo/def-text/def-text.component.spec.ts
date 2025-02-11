import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DefTextComponent } from './def-text.component';

describe('DefTextComponent', () => {
  let component: DefTextComponent;
  let fixture: ComponentFixture<DefTextComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DefTextComponent]
    });
    fixture = TestBed.createComponent(DefTextComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
