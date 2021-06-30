import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { IGenericoResponse } from 'src/app/models/GenericoResponseModel';

@Injectable({
  providedIn: 'root'
})
export class ReconhecerService {

  constructor(private http: HttpClient) { }
  objModel: any;
  sUrl = environment.baseUrl + environment.controllers.gestor.reconhecer;
  sUrlListar = this.sUrl + environment.routes.common.listar;
  sUrlConsultar = this.sUrl + environment.routes.common.consultar;
  sUrlGravar = this.sUrl + environment.routes.common.gravar;
  sUrlAtivarInativar = this.sUrl + environment.routes.common.situacao;

  ConfirmarRecomendacao(cs: string,
                        csSolicitante: string,
                        tipoRecomendacao: string,
                        pontos: number,
                        justificativa: string): Observable<IGenericoResponse> {

    this.objModel = new Object();
    this.objModel.cs = cs;
    this.objModel.cs_solicitante = csSolicitante;
    this.objModel.tipo_recomendacao = tipoRecomendacao;
    this.objModel.qtde_pontos = pontos;
    this.objModel.justificativa = justificativa;

    return this.http.post<IGenericoResponse>(this.sUrlGravar, this.objModel);
  }
}
