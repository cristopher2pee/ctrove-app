import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DefTableComponent } from './def-table.component';

describe('DefTableComponent', () => {
  let component: DefTableComponent;
  let fixture: ComponentFixture<DefTableComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DefTableComponent]
    });
    fixture = TestBed.createComponent(DefTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
