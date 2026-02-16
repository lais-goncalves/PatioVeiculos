import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListaVeiculos } from './lista-veiculos';

describe('ListaVeiculos', () => {
  let component: ListaVeiculos;
  let fixture: ComponentFixture<ListaVeiculos>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ListaVeiculos]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListaVeiculos);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
