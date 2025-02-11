import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageSitePhaseComponent } from './manage-site-phase.component';

describe('ManageSitePhaseComponent', () => {
  let component: ManageSitePhaseComponent;
  let fixture: ComponentFixture<ManageSitePhaseComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ManageSitePhaseComponent]
    });
    fixture = TestBed.createComponent(ManageSitePhaseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
