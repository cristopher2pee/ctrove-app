import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DefResultComponent } from './def-result.component';

describe('DefResultComponent', () => {
  let component: DefResultComponent;
  let fixture: ComponentFixture<DefResultComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DefResultComponent]
    });
    fixture = TestBed.createComponent(DefResultComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
