import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { IGenericoListResponse, IGenericoResponse } from 'src/app/models/GenericoResponseModel';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class HistoricoRecomendacoesService {

  constructor(private http: HttpClient) { }
  objModel: any;
  sUrl = environment.baseUrl + environment.controllers.gestor.reconhecer;
  sUrlListar = this.sUrl + 'listar-status-recomendacoes';
  sUrlConsultar = this.sUrl + 'detalhe-recomendacao';
  sUrlGravar = this.sUrl + environment.routes.common.gravar;
  sUrlAtivarInativar = this.sUrl + environment.routes.common.situacao;

  ObterRecomendacoes(cs: string) {
    return this.http.get<IGenericoResponse>(this.sUrlListar);
  }

  async ObterDetalheRecomendacao(id: string) {
    return this.http.get<IGenericoResponse>(this.sUrlConsultar + '?id=' + id).toPromise();
  }
}
