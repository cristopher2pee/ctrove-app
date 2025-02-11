import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DefAvatarComponent } from './def-avatar.component';

describe('DefAvatarComponent', () => {
  let component: DefAvatarComponent;
  let fixture: ComponentFixture<DefAvatarComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DefAvatarComponent]
    });
    fixture = TestBed.createComponent(DefAvatarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
