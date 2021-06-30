import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { IGenericoListResponse, IGenericoResponse } from 'src/app/models/GenericoResponseModel';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LogsService {

  constructor(private http: HttpClient) { }
  objModel: any;
  sUrl = environment.baseUrl + environment.controllers.admintec.logs;
  sUrlListar = this.sUrl + environment.routes.common.listar;
  sUrlConsultar = this.sUrl + environment.routes.common.consultar;
  sUrlGravar = this.sUrl + environment.routes.common.gravar;
  sUrlAtivarInativar = this.sUrl + environment.routes.common.situacao;

  ObterListaOperacoesLogs() {
    return this.http.get<IGenericoListResponse>(this.sUrl + 'listar-operacoes');
  }

  ObterLogs(dtInicio: string,
            dtFim: string,
            operacao: string,
            termo: string): Observable<IGenericoListResponse> {

    this.objModel = new Object();
    this.objModel.dataInicial = dtInicio;
    this.objModel.dataFinal = dtFim;
    this.objModel.operacao = operacao;
    this.objModel.observacoes = termo;

    return this.http.post<IGenericoListResponse>(this.sUrlListar, this.objModel);
  }
}
