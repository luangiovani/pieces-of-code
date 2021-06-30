import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { IGenericoListResponse } from 'src/app/models/GenericoResponseModel';

@Injectable({
  providedIn: 'root'
})
export class HistoricoTrocasService {

  constructor(private http: HttpClient) { }

  objModel: any;
  sUrl = environment.baseUrl + environment.controllers.loja.trocas;
  sUrlListar = this.sUrl + environment.routes.common.listar;
  sUrlConsultar = this.sUrl + environment.routes.common.consultar;
  sUrlGravar = this.sUrl + environment.routes.common.gravar;
  sUrlAtivarInativar = this.sUrl + environment.routes.common.situacao;

  ObterHistorico() {
    return this.http.get<IGenericoListResponse>(this.sUrl + 'historico-trocas');
  }
}
