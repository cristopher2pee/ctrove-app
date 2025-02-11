import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DefInputGroupComponent } from './def-input-group.component';

describe('DefInputGroupComponent', () => {
  let component: DefInputGroupComponent;
  let fixture: ComponentFixture<DefInputGroupComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DefInputGroupComponent]
    });
    fixture = TestBed.createComponent(DefInputGroupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
