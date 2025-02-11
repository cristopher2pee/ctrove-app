import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DefDrawerComponent } from './def-drawer.component';

describe('DefDrawerComponent', () => {
  let component: DefDrawerComponent;
  let fixture: ComponentFixture<DefDrawerComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DefDrawerComponent]
    });
    fixture = TestBed.createComponent(DefDrawerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
