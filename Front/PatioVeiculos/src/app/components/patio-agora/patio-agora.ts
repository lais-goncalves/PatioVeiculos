import {ChangeDetectorRef, Component, OnInit} from '@angular/core';
import {Titulo} from '../../shared/titulo/titulo'
import {VeiculosService} from '../../services/VeiculosService';
import {HttpService} from '../../services/HttpService';
import {RouterOutlet} from '@angular/router';

@Component({
  selector: 'app-patio-agora',
  imports: [RouterOutlet],
  templateUrl: './patio-agora.html',
  styleUrl: './patio-agora.scss',
  providers: [VeiculosService, HttpService]
})
export class PatioAgora implements OnInit {
  public veiculos: Veiculo[] = [];

  constructor(
    private veiculosService: VeiculosService,
    private changeDetector: ChangeDetectorRef
  ) {
  }

  ngOnInit() {
    this.buscarVeiculosNoPatio();
  }

  editarVeiculo(id: number): void {
    console.log(id)
  }

  buscarVeiculosNoPatio(): void {
    this.veiculosService.buscarVeiculosNoPatio().subscribe({
      next: (veiculos: Array<object>) => {
        this.veiculos = veiculos as Veiculo[];
        this.changeDetector.detectChanges();
      },
      error: err => {
        console.log(err)
      }
    });
  }
}
