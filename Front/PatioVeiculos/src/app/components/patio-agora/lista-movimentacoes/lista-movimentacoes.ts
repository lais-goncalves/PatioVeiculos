import {ChangeDetectorRef, Component, OnInit, signal, TemplateRef, WritableSignal} from '@angular/core';
import {VeiculosService} from '../../../services/VeiculosService';
import {Titulo} from '../../../shared/titulo/titulo';
import {FormsModule} from '@angular/forms';
import {Router} from '@angular/router';
import {ModalDismissReasons, NgbModal} from '@ng-bootstrap/ng-bootstrap';
import {inject} from 'vitest';
import {MovimentacoesService} from '../../../services/MovimentacoesService';

@Component({
  selector: 'app-lista-movimentacoes',
  imports: [
    Titulo,
    FormsModule
  ],
  templateUrl: './lista-movimentacoes.html',
  styleUrl: './lista-movimentacoes.scss',
})
export class ListaMovimentacoes implements OnInit {
  private _filtro: string = '';
  public veiculos: Veiculo[] = [];
  public veiculosFiltrados: Veiculo[] | null= null;
  public infoSaida: any = {};

  constructor(
    private veiculosService: VeiculosService,
    private movimentacoesService: MovimentacoesService,
    private changeDetector: ChangeDetectorRef,
    private router: Router,
    private modalService: NgbModal
  ) {
  }

  ngOnInit() {
    this.buscarVeiculosNoPatio();
  }

  cadastrarEntrada(): void {
    this.router.navigate(["patio-agora/cadastrar"]);
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

  public get filtro(): string {
    return this._filtro;
  }

  private normalizarString(str: string): string {
    return str.toLowerCase().trim();
  }

  public set filtro(value: string) {
    this._filtro = value;
    let novoFiltro: string = this.normalizarString(this._filtro);

    if (novoFiltro.length > 0) {
      this.veiculosFiltrados = this.filtrarEventos(novoFiltro);
    }

    else {
      this.veiculosFiltrados = null;
    }
  }

  public filtrarEventos(filtro: string): Veiculo[] {
    return this.veiculos.filter((v: any) =>
      this.normalizarString(v.placa).indexOf(filtro) !== -1);
  }

  definirMovimentacao(veiculoPlaca: string): Movimentacao {
    var tzoffset = (new Date()).getTimezoneOffset() * 60000;
    var dataAtual = new Date(Date.now() - tzoffset);

    var movimentacao: Movimentacao = {
      placaVeiculo: veiculoPlaca,
      dataHoraSaida: new Date(dataAtual)
    } as Movimentacao;

    return movimentacao;
  }

  fecharModal() {
    this.modalService.dismissAll();
    this.infoSaida = {};
  }

  abrirModal(content: TemplateRef<any>, veiculoPlaca: string) {
    var movimentacao: Movimentacao = this.definirMovimentacao(veiculoPlaca);

    this.movimentacoesService.buscarInfoSaidaVeiculo(movimentacao).subscribe({
      next: (info: any) => {
        this.infoSaida = info;
        this.changeDetector.detectChanges();
        this.modalService.open(content);
      },
      error: err => { console.log(err) }
    })
  }

  registrarSaida(veiculoPlaca: string): void {
    var movimentacao: Movimentacao = this.definirMovimentacao(veiculoPlaca);

    this.movimentacoesService.cadastrarSaida(movimentacao).subscribe({
      next: (r: any) => {
        console.log(r);
        this.fecharModal();
        window.location.reload();
      },
      error: err => { console.log(err) }
    })
  }
}
