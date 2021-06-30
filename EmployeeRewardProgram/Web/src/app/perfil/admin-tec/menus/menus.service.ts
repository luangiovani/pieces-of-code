import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { IGenericoListResponse, IGenericoResponse } from 'src/app/models/GenericoResponseModel';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MenusService {

  constructor(private http: HttpClient) { }

  objModel: any;
  sUrl = environment.baseUrl + environment.controllers.admintec.menu;
  sUrlListar = this.sUrl + environment.routes.common.listar;
  sUrlConsultar = this.sUrl + environment.routes.common.consultar;
  sUrlGravar = this.sUrl + environment.routes.common.gravar;
  sUrlAtivarInativar = this.sUrl + environment.routes.common.situacao;

  ObterMenus() {
    return this.http.get<IGenericoListResponse>(this.sUrlListar);
  }

  ObterMenu(id: string = '') {
    return this.http.get<IGenericoResponse>(this.sUrlConsultar + '?id=' + id);
  }

  ConfirmarMenu(id: string,
                aplicacaoId: string,
                menuSuperiorId: string,
                nome: string,
                controller: string,
                acao: string,
                icone: string,
                ativo: string,
                colaborador: string): Observable<IGenericoResponse> {


    this.objModel = new Object();
    this.objModel.id = id;
    this.objModel.aplicacao_id = aplicacaoId;
    this.objModel.menu_superior_id = menuSuperiorId;
    this.objModel.nome = nome;
    this.objModel.controller = controller;
    this.objModel.acao = acao;
    this.objModel.icone = icone;
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
