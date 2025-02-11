import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DefIconComponent } from './def-icon.component';

describe('DefIconComponent', () => {
  let component: DefIconComponent;
  let fixture: ComponentFixture<DefIconComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DefIconComponent]
    });
    fixture = TestBed.createComponent(DefIconComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
