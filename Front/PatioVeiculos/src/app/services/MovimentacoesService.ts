import {HttpService} from './HttpService';
import {Observable} from 'rxjs';
import {Injectable} from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class MovimentacoesService {
  constructor(private httpService: HttpService) {}

  public buscarInfoSaidaVeiculo(movimentacao: Movimentacao): Observable<Array<object>> {
    return this.httpService.enviarPOST('/Movimentacoes/info-saida', movimentacao);
  }

  public cadastrarEntrada(movimentacao: Movimentacao): Observable<Array<object>> {
    return this.httpService.enviarPOST('/Movimentacoes/cadastrar-entrada', movimentacao);
  }

  public cadastrarSaida(movimentacao: Movimentacao): Observable<Array<object>> {
    return this.httpService.enviarPOST('/Movimentacoes/cadastrar-saida', movimentacao);
  }
}
