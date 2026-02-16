import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditarVeiculo } from './editar-veiculo';

describe('EditarVeiculo', () => {
  let component: EditarVeiculo;
  let fixture: ComponentFixture<EditarVeiculo>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditarVeiculo]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditarVeiculo);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
