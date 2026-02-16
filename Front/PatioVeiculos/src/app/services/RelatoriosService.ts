import {HttpService} from './HttpService';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {HttpParams} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class RelatoriosService {
  constructor(private httpService: HttpService) { }

  public buscarFaturamentoPorDia(dias: number = 7): Observable<Array<object>> {
    const params = new HttpParams().set('dias', dias.toString());
    return this.httpService.enviarGET(`/Relatorios/faturamento/${dias}`, params);
  }

  public buscarTop10VeiculosPorTempo(dataInicio: string, dataFim: string): Observable<Array<object>> {
    const params = new HttpParams()
      .set('dataInicio', dataInicio)
      .set('dataFim', dataFim);
    return this.httpService.enviarGET('/Relatorios/top10-tempo', params);
  }

  public buscarOcupacaoPorHora(dataInicio: string, dataFim: string): Observable<Array<object>> {
    const params = new HttpParams()
      .set('dataInicio', dataInicio)
      .set('dataFim', dataFim);
    return this.httpService.enviarGET('/Relatorios/ocupacao-hora', params);
  }
}
