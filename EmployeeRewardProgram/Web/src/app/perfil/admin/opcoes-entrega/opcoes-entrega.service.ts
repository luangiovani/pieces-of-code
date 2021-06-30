import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { IGenericoListResponse, IGenericoResponse } from 'src/app/models/GenericoResponseModel';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class OpcoesEntregaService {

  constructor(private http: HttpClient) { }
  objModel: any;
  sUrl = environment.baseUrl + environment.controllers.admin.opcaoentrega;
  sUrlListar = this.sUrl + environment.routes.common.listar;
  sUrlListarPorLoja = this.sUrl + 'listar-por-loja';
  sUrlConsultar = this.sUrl + environment.routes.common.consultar;
  sUrlGravar = this.sUrl + environment.routes.common.gravar;
  sUrlAtivarInativar = this.sUrl + environment.routes.common.situacao;

  ObterOpcoesEntrega() {
    return this.http.get<IGenericoListResponse>(this.sUrlListar);
  }

  ObterOpcoesEntregaPorLoja(lojaid: string = '') {
    return this.http.get<IGenericoListResponse>(this.sUrlListarPorLoja + '?lojaid=' + lojaid);
  }

  async ObterOpcaoEntrega(id: string = '') {
    const opcao = this.http.get<IGenericoResponse>(this.sUrlConsultar + '?id=' + id);
    return opcao.toPromise();
  }

  ConfirmarOpcaoEntrega(id: string,
                        ativo: string,
                        label: string,
                        labelColaborador: string,
                        labelLoja: string,
                        colaborador: string): Observable<IGenericoResponse> {

    this.objModel = new Object();
    this.objModel.id = id;
    this.objModel.ativo = (id === '' ? 'true' : ativo);
    this.objModel.label = label;
    this.objModel.label_colaborador = labelColaborador;
    this.objModel.label_loja = labelLoja;
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
