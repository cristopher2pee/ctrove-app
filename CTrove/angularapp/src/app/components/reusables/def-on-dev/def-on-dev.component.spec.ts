import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DefOnDevComponent } from './def-on-dev.component';

describe('DefOnDevComponent', () => {
  let component: DefOnDevComponent;
  let fixture: ComponentFixture<DefOnDevComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DefOnDevComponent]
    });
    fixture = TestBed.createComponent(DefOnDevComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
