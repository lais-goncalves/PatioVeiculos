import {ChangeDetectorRef, Component, OnInit} from '@angular/core';
import {AbstractControl, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {Titulo} from "../../../shared/titulo/titulo";
import {VeiculosService} from '../../../services/VeiculosService';
import {ActivatedRoute, Router} from '@angular/router';
import {placaUnicaValidator} from '../../../form-validators/placa-unica.validator';
import {NgClass} from '@angular/common';

@Component({
  selector: 'app-cadastrar-veiculo',
  imports: [
    FormsModule,
    ReactiveFormsModule,
    Titulo,
    NgClass
  ],
  templateUrl: './cadastrar-veiculo.html',
  styleUrl: './cadastrar-veiculo.scss',
})
export class CadastrarVeiculo implements OnInit {
  public veiculos: Veiculo = {} as Veiculo;
  public form: FormGroup = new FormGroup({});

  get controls(): any {
    return this.form.controls;
  }

  constructor(
    private veiculosService: VeiculosService,
    private router: Router
  ) {}

  ngOnInit() {
    this.definirForm();
  }

  public validarCampoVeiculos(control: string) {
    return this.validarCampo(this.form.get(control));
  }

  protected validarCampo(control: AbstractControl | null) {
    const invalido: boolean = (control?.errors && control?.touched) || false;
    return {'is-invalid': invalido};
  }

  definirForm(): void {
    this.form = new FormGroup({
      placa: new FormControl(
        null,
        [
          Validators.required,
          Validators.minLength(7),
          Validators.maxLength(9),
        ],
        [placaUnicaValidator(this.veiculosService, false)]
      ),
      modelo:  new FormControl(null, [
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(20),
      ]),
      cor: new FormControl(null, [
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(20),
      ]),
      tipo: new FormControl(null, [
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(20),
      ])
    });
  }

  cadastrarVeiculo(): void {
    if (this.form.invalid) {
      console.log('invalid')
      return;
    }

    let valores = this.form.value;

    this.veiculosService.cadastrarVeiculo(valores).subscribe({
      next: (veiculo: any) => {
        this.form.reset();
        this.router.navigate(['/veiculos/lista']);
      },
      error: (err) => { console.log(err); }
    })
  }
}
