import {HttpService} from './HttpService';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class VeiculosService {
  constructor(private httpService: HttpService) { }

  public buscarTodosOsVeiculos(): Observable<Array<object>> {
    return this.httpService.enviarGET('/Veiculos');
  }

  public buscarVeiculosNoPatio(): Observable<Array<object>> {
    return this.httpService.enviarGET('/Veiculos/no-patio');
  }

  public buscarVeiculoPorId(id: number): Observable<Array<object>> {
    return this.httpService.enviarGET(`/Veiculos/id/${id}`);
  }

  public buscarVeiculoPorPlaca(placa: string): Observable<Array<object>> {
    return this.httpService.enviarGET(`/Veiculos/placa/${placa}`); // TODO: trocar pela controller
  }

  public verificarVeiculoNoPatio(placa: string): Observable<Array<object>> {
    return this.httpService.enviarGET(`/Veiculos/no-patio/${placa}`); // TODO: trocar pela controller
  }

  public verificarPlacaExiste(placa: string): Observable<Array<object>> {
    return this.httpService.enviarGET(`/Veiculos/verificar-placa/${placa}`); // TODO: trocar pela controller
  }

  public editarVeiculo(id: number, veiculo: Veiculo): Observable<Array<object>> {
    return this.httpService.enviarPUT(`/Veiculos/editar/${id}`, veiculo);
  }

  public cadastrarVeiculo(veiculo: Veiculo): Observable<Array<object>> {
    return this.httpService.enviarPOST(`/Veiculos/cadastrar`, veiculo);
  }
}
