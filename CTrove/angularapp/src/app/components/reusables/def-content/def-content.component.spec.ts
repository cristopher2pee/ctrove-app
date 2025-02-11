import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DefContentComponent } from './def-content.component';

describe('DefContentComponent', () => {
  let component: DefContentComponent;
  let fixture: ComponentFixture<DefContentComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DefContentComponent]
    });
    fixture = TestBed.createComponent(DefContentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
