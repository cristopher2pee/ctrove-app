import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DefCardComponent } from './def-card.component';

describe('DefCardComponent', () => {
  let component: DefCardComponent;
  let fixture: ComponentFixture<DefCardComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DefCardComponent]
    });
    fixture = TestBed.createComponent(DefCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
