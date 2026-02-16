import { AbstractControl, AsyncValidatorFn, ValidationErrors } from '@angular/forms';
import { Observable, of } from 'rxjs';
import { map, catchError, debounceTime, switchMap, first } from 'rxjs/operators';
import {VeiculosService} from '../services/VeiculosService';

export function placaNoPatioValidator(
  veiculosService: VeiculosService,
): AsyncValidatorFn {
  return (control: AbstractControl): Observable<ValidationErrors | null> => {
    let tempoPararDeEditar = 500;

    return of(control.value).pipe(
      debounceTime(tempoPararDeEditar),
      switchMap(placa =>
        veiculosService.verificarVeiculoNoPatio(placa).pipe(
          map((placa: any)=> {
            if (placa?.noPatio) {
              return { jaEstaNoPatio: true };
            }

            return null;
          }))
      ), first()
    );
  };
}
