import {ChangeDetectorRef, Component, OnInit} from '@angular/core';
import {FormsModule} from "@angular/forms";
import {Titulo} from "../../../shared/titulo/titulo";
import {VeiculosService} from '../../../services/VeiculosService';
import {Router} from '@angular/router';

@Component({
  selector: 'app-lista-veiculos',
    imports: [
        FormsModule,
        Titulo
    ],
  templateUrl: './lista-veiculos.html',
  styleUrl: './lista-veiculos.scss',
})
export class ListaVeiculos implements OnInit {
  public veiculos: Veiculo[] = [];

  constructor(
    private veiculosService: VeiculosService,
    private changeDetector: ChangeDetectorRef,
    private router: Router
  ) {}

  ngOnInit() {
    this.buscarTodosOsVeiculos();
  }

  editarVeiculo(id: number): void {
    this.router.navigate([`veiculos/editar/${id}`]);
  }

  cadastrarVeiculo(): void {
    this.router.navigate([`veiculos/cadastrar`]);
  }

  buscarTodosOsVeiculos(): void {
    this.veiculosService.buscarTodosOsVeiculos().subscribe({
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
