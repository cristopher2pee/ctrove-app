import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DefLoadingScreenComponent } from './def-loading-screen.component';

describe('DefLoadingScreenComponent', () => {
  let component: DefLoadingScreenComponent;
  let fixture: ComponentFixture<DefLoadingScreenComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DefLoadingScreenComponent]
    });
    fixture = TestBed.createComponent(DefLoadingScreenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
