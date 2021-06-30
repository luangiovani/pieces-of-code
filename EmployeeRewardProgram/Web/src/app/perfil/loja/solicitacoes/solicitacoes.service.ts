import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { IGenericoResponse } from 'src/app/models/GenericoResponseModel';

@Injectable({
  providedIn: 'root'
})
export class SolicitacoesService {

  constructor(private http: HttpClient) { }
  objModel: any;
  sUrl = environment.baseUrl + environment.controllers.loja.trocas;
  sUrlListar = this.sUrl + environment.routes.common.listar;
  sUrlConsultar = this.sUrl + environment.routes.common.consultar;
  sUrlGravar = this.sUrl + environment.routes.common.gravar;
  sUrlAtivarInativar = this.sUrl + environment.routes.common.situacao;

  ObterSolicitacoesPendentes() {
    return this.http.get<IGenericoResponse>(this.sUrl + 'trocas-pendentes');
  }

  ObterSolicitacao(id: string = '') {
    if (id !== '' && id !== undefined && id !== null) {
      return this.http.get<IGenericoResponse>(this.sUrl + 'obter-solicitacao-pendente?id=' + id);
    }
  }

  AtualizarSolicitacao(id: string = '', situacaoId: string = '', justificativa: string = '', infcomplementar: string = '') {

    this.objModel = new Object();
    this.objModel.compra_id = id;
    this.objModel.situacao_compra_id = situacaoId;
    this.objModel.justificativa = justificativa;
    this.objModel.informacoes_complementares = infcomplementar;

    return this.http.post<IGenericoResponse>(this.sUrl + 'mudar-solicitacao-pendente', this.objModel);
  }
}
