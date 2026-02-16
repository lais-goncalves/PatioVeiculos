import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RelatorioFaturamento } from './relatorio-faturamento';

describe('RelatorioFaturamento', () => {
  let component: RelatorioFaturamento;
  let fixture: ComponentFixture<RelatorioFaturamento>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RelatorioFaturamento]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RelatorioFaturamento);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
