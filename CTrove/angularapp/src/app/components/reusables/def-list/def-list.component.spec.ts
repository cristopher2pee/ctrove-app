import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DefListComponent } from './def-list.component';

describe('DefListComponent', () => {
  let component: DefListComponent;
  let fixture: ComponentFixture<DefListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DefListComponent]
    });
    fixture = TestBed.createComponent(DefListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
