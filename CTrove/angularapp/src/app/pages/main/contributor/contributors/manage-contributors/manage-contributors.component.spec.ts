import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageContributorsComponent } from './manage-contributors.component';

describe('ManageContributorsComponent', () => {
  let component: ManageContributorsComponent;
  let fixture: ComponentFixture<ManageContributorsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ManageContributorsComponent]
    });
    fixture = TestBed.createComponent(ManageContributorsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
