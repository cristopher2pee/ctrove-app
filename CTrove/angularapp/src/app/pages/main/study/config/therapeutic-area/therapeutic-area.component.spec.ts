import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TherapeuticAreaComponent } from './therapeutic-area.component';

describe('TherapeuticAreaComponent', () => {
  let component: TherapeuticAreaComponent;
  let fixture: ComponentFixture<TherapeuticAreaComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TherapeuticAreaComponent]
    });
    fixture = TestBed.createComponent(TherapeuticAreaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
