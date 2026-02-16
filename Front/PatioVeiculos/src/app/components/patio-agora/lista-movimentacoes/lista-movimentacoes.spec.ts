import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListaMovimentacoes } from './lista-movimentacoes';

describe('ListaMovimentacoes', () => {
  let component: ListaMovimentacoes;
  let fixture: ComponentFixture<ListaMovimentacoes>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ListaMovimentacoes]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListaMovimentacoes);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
