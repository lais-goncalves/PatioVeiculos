import {ChangeDetectorRef, Component, OnInit} from '@angular/core';
import {AbstractControl, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {Titulo} from "../../../shared/titulo/titulo";
import {VeiculosService} from '../../../services/VeiculosService';
import {ActivatedRoute, Router} from '@angular/router';
import {placaUnicaValidator} from '../../../form-validators/placa-unica.validator';
import {NgClass} from '@angular/common';

@Component({
  selector: 'app-editar-veiculo',
  imports: [
    FormsModule,
    ReactiveFormsModule,
    Titulo,
    NgClass
  ],
  templateUrl: './editar-veiculo.html',
  styleUrl: './editar-veiculo.scss',
})
export class EditarVeiculo implements OnInit {
  public veiculo: Veiculo = {} as Veiculo;
  public form: FormGroup = new FormGroup({});

  get controls(): any {
    return this.form.controls;
  }

  constructor(
    private veiculosService: VeiculosService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private changeDetector : ChangeDetectorRef
  ) {}

  ngOnInit() {
    this.buscarVeiculo();
    this.definirForm();
  }

  buscarVeiculo(): void {
    const veiculoId = this.activatedRoute.snapshot.params['id'];

    this.veiculosService.buscarVeiculoPorId(veiculoId).subscribe({
      next: (veiculo: any) => {
        if (!veiculo || !veiculo.id) {
          this.router.navigate(['/veiculos/lista']);
        }

        this.veiculo = veiculo as Veiculo;
        this.changeDetector.detectChanges();
        this.form.patchValue(veiculo);
      },
      error: (err) => { console.log(err); }
    })
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

  salvarAlteracoes(): void {
    if (this.form.invalid) {
      return;
    }

    let valores = this.form.value;

    this.veiculosService.editarVeiculo(this.veiculo.id, valores).subscribe({
      next: (veiculo: any) => {
        window.location.reload();
      },
      error: (err) => { console.log(err); }
    })
  }
}
