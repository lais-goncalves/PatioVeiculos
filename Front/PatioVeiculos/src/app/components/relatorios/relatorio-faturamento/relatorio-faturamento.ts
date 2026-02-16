import {ChangeDetectorRef, Component, OnInit} from '@angular/core';
import {RelatoriosService} from '../../../services/RelatoriosService';
import {FormControl, FormGroup, ReactiveFormsModule} from '@angular/forms';

@Component({
  selector: 'app-relatorio-faturamento',
  imports: [
    ReactiveFormsModule
  ],
  templateUrl: './relatorio-faturamento.html',
  styleUrl: './relatorio-faturamento.scss',
})
export class RelatorioFaturamento implements OnInit {
  public form: FormGroup = new FormGroup({});
  public faturamento: any = {};
  public dias: number = 7;

  constructor(
    private relatoriosService: RelatoriosService,
    private changeDetectorRef: ChangeDetectorRef,
  ) {}

  ngOnInit() {
    this.definirFormulario();
    this.buscarInfoFaturamento(this.dias);
  }

  definirFormulario(): void {
    this.form = new FormGroup({
      qtdDias: new FormControl(this.dias.toString())
    });
  }

  mudouValorRadio(qtdDias: number) {
    this.buscarInfoFaturamento(qtdDias);
  }

  buscarInfoFaturamento(qtdDias: number): void {
    this.relatoriosService.buscarFaturamentoPorDia(qtdDias).subscribe({
      next: (info: Array<any>) => {
        this.faturamento = info[0];
        this.changeDetectorRef.detectChanges();
        console.log(this.faturamento);
      },
      error: err => { console.log(err) },
    })
  }
}
