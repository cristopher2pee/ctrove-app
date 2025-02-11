import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DefSkeletonElementComponent } from './def-skeleton-element.component';

describe('DefSkeletonElementComponent', () => {
  let component: DefSkeletonElementComponent;
  let fixture: ComponentFixture<DefSkeletonElementComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DefSkeletonElementComponent]
    });
    fixture = TestBed.createComponent(DefSkeletonElementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
