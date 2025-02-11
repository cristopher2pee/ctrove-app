import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DefDividerComponent } from './def-divider.component';

describe('DefDividerComponent', () => {
  let component: DefDividerComponent;
  let fixture: ComponentFixture<DefDividerComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DefDividerComponent]
    });
    fixture = TestBed.createComponent(DefDividerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
