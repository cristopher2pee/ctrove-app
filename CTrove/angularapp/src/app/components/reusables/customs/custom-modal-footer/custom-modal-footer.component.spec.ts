import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomModalFooterComponent } from './custom-modal-footer.component';

describe('CustomModalFooterComponent', () => {
  let component: CustomModalFooterComponent;
  let fixture: ComponentFixture<CustomModalFooterComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CustomModalFooterComponent]
    });
    fixture = TestBed.createComponent(CustomModalFooterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
