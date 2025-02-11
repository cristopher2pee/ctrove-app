import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DefLayoutComponent } from './def-layout.component';

describe('DefLayoutComponent', () => {
  let component: DefLayoutComponent;
  let fixture: ComponentFixture<DefLayoutComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DefLayoutComponent]
    });
    fixture = TestBed.createComponent(DefLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
