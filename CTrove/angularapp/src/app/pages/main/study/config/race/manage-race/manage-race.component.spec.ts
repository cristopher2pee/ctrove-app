import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageRaceComponent } from './manage-race.component';

describe('ManageRaceComponent', () => {
  let component: ManageRaceComponent;
  let fixture: ComponentFixture<ManageRaceComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ManageRaceComponent]
    });
    fixture = TestBed.createComponent(ManageRaceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
