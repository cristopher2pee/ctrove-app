import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DefSkeletonComponent } from './def-skeleton.component';

describe('DefSkeletonComponent', () => {
  let component: DefSkeletonComponent;
  let fixture: ComponentFixture<DefSkeletonComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DefSkeletonComponent]
    });
    fixture = TestBed.createComponent(DefSkeletonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
