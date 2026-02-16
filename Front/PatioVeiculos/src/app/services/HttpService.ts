import {environment} from '../../environments/environment';
import {enableProdMode, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HttpService {
  constructor(private http: HttpClient) {
    if (this._ambienteProd) {
      enableProdMode();
    }
  }

  get _ambienteProd() {
    return environment.production;
  }

  get apiUrl() {
    if (this._ambienteProd) {
      return environment.apiProd;
    }

    return environment.apiDev;
  }

  public enviarGET(url: string, opcoes: any = {}): Observable<any> {
    return this.http.get(this.apiUrl + url, opcoes);
  }

  public enviarPOST(url: string, body: any, opcoes: any = {}): Observable<any> {
    return this.http.post(this.apiUrl + url, body, opcoes);
  }

  public enviarPUT(url: string, body: any, opcoes: any = {}): Observable<any> {
    return this.http.put(this.apiUrl + url, body, opcoes);
  }
}
