import { AbstractControl, AsyncValidatorFn, ValidationErrors } from '@angular/forms';
import { Observable, of } from 'rxjs';
import { map, catchError, debounceTime, switchMap, first } from 'rxjs/operators';
import {VeiculosService} from '../services/VeiculosService';

export function placaUnicaValidator(
  veiculosService: VeiculosService,
  deveExistir: boolean = true
): AsyncValidatorFn {
  return (control: AbstractControl): Observable<ValidationErrors | null> => {
    let tempoPararDeEditar = 1000;

    return of(control.value).pipe(
      debounceTime(tempoPararDeEditar),
      switchMap(placa => {
        return veiculosService.verificarPlacaExiste(placa).pipe(
          map((placa: any)=> {
            console.log(placa);

            if (deveExistir) {
              if (!placa?.estaCadastrada) {
                return { placaNaoExiste: true };
              }
              return null;
            }

            else {
              if (placa?.estaCadastrada) {
                return { placaJaExiste: true }
              }
              return null;
            }
          }))
      }), first()
    );
  };
}
