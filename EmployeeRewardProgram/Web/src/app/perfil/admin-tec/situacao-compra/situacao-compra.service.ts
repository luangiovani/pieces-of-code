import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { IGenericoListResponse, IGenericoResponse } from 'src/app/models/GenericoResponseModel';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SituacaoCompraService {

  constructor(private http: HttpClient) { }
  objModel: any;
  sUrl = environment.baseUrl + environment.controllers.admintec.situacaoCompra;
  sUrlListar = this.sUrl + environment.routes.common.listar;
  sUrlConsultar = this.sUrl + environment.routes.common.consultar;
  sUrlGravar = this.sUrl + environment.routes.common.gravar;
  sUrlAtivarInativar = this.sUrl + environment.routes.common.situacao;

  ObterSituacoesCompra() {
    return this.http.get<IGenericoListResponse>(this.sUrlListar);
  }

  async ObterSituacoesCompraAsync() {
    return this.http.get<IGenericoListResponse>(this.sUrlListar).toPromise();
  }

  ObterSituacaoCompra(id: string = '') {
    return this.http.get<IGenericoResponse>(this.sUrlConsultar + '?id=' + id);
  }


  ConfirmarSituacaoCompra(id: string,
                          descricao: string,
                          ativo: string,
                          colaborador: string): Observable<IGenericoResponse> {

    this.objModel = new Object();
    this.objModel.id = id;
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
