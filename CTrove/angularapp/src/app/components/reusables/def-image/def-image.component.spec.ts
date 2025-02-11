import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DefImageComponent } from './def-image.component';

describe('DefImageComponent', () => {
  let component: DefImageComponent;
  let fixture: ComponentFixture<DefImageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DefImageComponent]
    });
    fixture = TestBed.createComponent(DefImageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
