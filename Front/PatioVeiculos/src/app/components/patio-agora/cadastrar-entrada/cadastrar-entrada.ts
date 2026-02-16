import {Component, OnInit} from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';
import {Titulo} from '../../../shared/titulo/titulo';
import {placaUnicaValidator} from '../../../form-validators/placa-unica.validator';
import {VeiculosService} from '../../../services/VeiculosService';
import {NgClass} from '@angular/common';
import {placaNoPatioValidator} from '../../../form-validators/placa-no-patio.validator';
import {MovimentacoesService} from '../../../services/MovimentacoesService';
import {Router} from '@angular/router';

@Component({
  selector: 'app-cadastrar-entrada',
  imports: [
    FormsModule,
    ReactiveFormsModule,
    Titulo,
    NgClass
  ],
  templateUrl: './cadastrar-entrada.html',
  styleUrl: './cadastrar-entrada.scss',
})
export class CadastrarEntrada implements OnInit {
  public form: FormGroup = new FormGroup({});

  get controls(): any {
    return this.form.controls;
  }

  constructor(
    private veiculosService: VeiculosService,
    private movimentacoesService: MovimentacoesService,
    private router: Router
  ) {
  }

  ngOnInit() {
    this.definirForm();
  }

  definirForm(): void {
    var tzoffset = (new Date()).getTimezoneOffset() * 60000;
    var dataAtual = (new Date(Date.now() - tzoffset)).toISOString().slice(0,16);

    this.form = new FormGroup({
      placaVeiculo: new FormControl(
        null,
        [Validators.required],
        [
          placaUnicaValidator(this.veiculosService, true),
          placaNoPatioValidator(this.veiculosService)
        ]
      ),
      dataHoraEntrada: new FormControl(dataAtual, [Validators.required])
    });
  }

  public validarCampoVeiculos(control: string) {
    return this.validarCampo(this.form.get(control));
  }

  protected validarCampo(control: AbstractControl | null) {
    const invalido: boolean = (control?.errors && control?.touched) || false;
    return {'is-invalid': invalido};
  }

  cadastrarEntrada() {
    if (this.form.invalid) {
      return;
    }

    const movimentacao = this.form.value;

    this.movimentacoesService.cadastrarEntrada(movimentacao).subscribe({
      next: movimentacao => {
        this.router.navigate(['/patio-agora']);
      },
      error: error => {
        console.log(error);
      }
    })
  }
}
