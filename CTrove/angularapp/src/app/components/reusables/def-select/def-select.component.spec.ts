import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DefSelectComponent } from './def-select.component';

describe('DefSelectComponent', () => {
  let component: DefSelectComponent;
  let fixture: ComponentFixture<DefSelectComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DefSelectComponent]
    });
    fixture = TestBed.createComponent(DefSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
