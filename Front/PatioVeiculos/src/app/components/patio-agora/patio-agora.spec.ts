import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PatioAgora } from './patio-agora';

describe('PatioAgora', () => {
  let component: PatioAgora;
  let fixture: ComponentFixture<PatioAgora>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PatioAgora]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PatioAgora);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
