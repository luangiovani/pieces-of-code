import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { IGenericoListResponse, IGenericoResponse } from 'src/app/models/GenericoResponseModel';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SituacaoRecomendacaoService {

  constructor(private http: HttpClient) { }
  objModel: any;
  sUrl = environment.baseUrl + environment.controllers.admintec.situacaoRecomendacao;
  sUrlListar = this.sUrl + environment.routes.common.listar;
  sUrlConsultar = this.sUrl + environment.routes.common.consultar;
  sUrlGravar = this.sUrl + environment.routes.common.gravar;
  sUrlAtivarInativar = this.sUrl + environment.routes.common.situacao;

  ObterSituacoesRecomendacao() {
    return this.http.get<IGenericoListResponse>(this.sUrlListar);
  }

  ObterSituacaoRecomendacao(id: string = '') {
    return this.http.get<IGenericoResponse>(this.sUrlConsultar + '?id=' + id);
  }


  ConfirmarSituacaoRecomendacao(id: string,
                                nome: string,
                                descricao: string,
                                ativo: string,
                                colaborador: string): Observable<IGenericoResponse> {

    this.objModel = new Object();
    this.objModel.id = id;
    this.objModel.nome = nome;
    this.objModel.descricao = descricao;
    this.objModel.ativo = (id === '' ? 'true' : ativo);
    this.objModel.cs_colaborador_criacao = colaborador;

    return this.http.post<IGenericoResponse>(this.sUrlGravar, this.objModel);
  }

  AtivarInativar(id: string, ativar: string) {

    this.objModel = new Object();
    this.objModel.id = id;
    this.objModel.ativar = ativar;
    return this.http.post<IGenericoResponse>(this.sUrlAtivarInativar, this.objModel);
  }

}
