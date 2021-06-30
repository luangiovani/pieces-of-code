import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { IGenericoListResponse, IGenericoResponse } from 'src/app/models/GenericoResponseModel';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class AvaliarService {

  constructor(private http: HttpClient) { }
  objModel: any;
  sUrl = environment.baseUrl + environment.controllers.gestor.avaliar;
  sUrlListar = this.sUrl + environment.routes.common.listar;
  sUrlConsultar = this.sUrl + environment.routes.common.consultar;
  sUrlGravar = this.sUrl + environment.routes.common.gravar;
  sUrlAtivarInativar = this.sUrl + environment.routes.common.situacao;
  sUrlAprovarReprovar = this.sUrl + environment.routes.common.efetivar;

  ObterRecomendacoes(cs: string) {
    return this.http.get<IGenericoListResponse>(this.sUrlListar);
  }

  async ObterDetalheRecomendacao(id: string) {
    return this.http.get<IGenericoResponse>(this.sUrlConsultar + '?id=' + id).toPromise();
  }

  AvaliarRecomendacao(id: string, justificativa: string, aprovar: string) {
    this.objModel = new Object();
    this.objModel.id = id;
    this.objModel.aprovar = (aprovar === '' ? 'false' : (aprovar === 'A') ? 'true' : 'false');
    this.objModel.justificativa = justificativa;

    return this.http.post<IGenericoResponse>(this.sUrlAprovarReprovar, this.objModel);
  }

  ObterAvaliacoesRealizadas() {
    return this.http.get<IGenericoListResponse>(this.sUrl + 'listar-avaliacoes-realizadas');
  }
}
