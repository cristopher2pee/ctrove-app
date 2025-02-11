import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomSitesSwitcherComponent } from './custom-sites-switcher.component';

describe('CustomSitesSwitcherComponent', () => {
  let component: CustomSitesSwitcherComponent;
  let fixture: ComponentFixture<CustomSitesSwitcherComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CustomSitesSwitcherComponent]
    });
    fixture = TestBed.createComponent(CustomSitesSwitcherComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
