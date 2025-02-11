import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DefUserProfileComponent } from './def-user-profile.component';

describe('DefUserProfileComponent', () => {
  let component: DefUserProfileComponent;
  let fixture: ComponentFixture<DefUserProfileComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DefUserProfileComponent]
    });
    fixture = TestBed.createComponent(DefUserProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
