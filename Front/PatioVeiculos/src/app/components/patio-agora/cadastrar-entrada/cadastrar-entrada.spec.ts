import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CadastrarEntrada } from './cadastrar-entrada';

describe('CadastrarEntrada', () => {
  let component: CadastrarEntrada;
  let fixture: ComponentFixture<CadastrarEntrada>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CadastrarEntrada]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CadastrarEntrada);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
