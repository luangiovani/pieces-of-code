import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { IGenericoListResponse, IGenericoResponse } from 'src/app/models/GenericoResponseModel';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LojasService {

  constructor(private http: HttpClient) { }
  objModel: any;
  sUrl = environment.baseUrl + environment.controllers.admin.lojas;
  sUrlListar = this.sUrl + environment.routes.common.listar;
  sUrlConsultar = this.sUrl + environment.routes.common.consultar;
  sUrlGravar = this.sUrl + environment.routes.common.gravar;
  sUrlAtivarInativar = this.sUrl + environment.routes.common.situacao;
  sUrlColaboradoresLoja = this.sUrl + 'obter-colaboradores-loja';
  sUrlGravarVinculoColaboradorLoja = this.sUrl + 'gravar-vinculo-colaborador-loja';
  sUrlDesvincularColaboradorLoja = this.sUrl + 'desvincular-colaborador-loja';
  ObterLojas() {
    return this.http.get<IGenericoListResponse>(this.sUrlListar);
  }

  async ObterLojasAsync() {
    return this.http.get<IGenericoListResponse>(this.sUrlListar).toPromise();
  }

  ObterLoja(id: string = '') {
    return this.http.get<IGenericoResponse>(this.sUrlConsultar + '?id=' + id);
  }

  ObterColaboradoresLoja() {
    return this.http.get<IGenericoListResponse>(this.sUrlColaboradoresLoja);
  }

  async ObterColaboradoresLojaAsync() {
    return this.http.get<IGenericoListResponse>(this.sUrlColaboradoresLoja).toPromise();
  }

  ObterColaborador(cs: string = '', lojaId: string = '') {
    return this.http.get<IGenericoResponse>(this.sUrlColaboradoresLoja + '?lojaId=' + lojaId + ' &cs=' + cs);
  }

  async ObterColaboradorAsync(cs: string = '', lojaId: string = '') {
    return this.http.get<IGenericoResponse>(this.sUrlColaboradoresLoja + '?lojaId=' + lojaId + ' &cs=' + cs).toPromise();
  }

  DesvincularColaboradorLoja(id: string) {
    return this.http.get<IGenericoResponse>(this.sUrlDesvincularColaboradorLoja + '?id=' + id);
  }

  ConfirmarColaboradorLoja(id: string = '', cs: string, lojaId: string) {
    this.objModel = new Object();
    this.objModel.id = id;
    this.objModel.cs = cs;
    this.objModel.loja_id = lojaId;

    return this.http.post<IGenericoResponse>(this.sUrlGravarVinculoColaboradorLoja, this.objModel);
  }

  ConfirmarLoja(id: string,
                nome: string,
                status: string,
                codigo: string,
                observacao: string,
                complemento: string,
                localizacao: string,
                ativo: string,
                colaborador: string): Observable<IGenericoResponse> {

    this.objModel = new Object();
    this.objModel.id = id;
    this.objModel.nome = nome;
    this.objModel.status_loja = status;
    this.objModel.codigo = codigo;
    this.objModel.observacao = observacao;
    this.objModel.complemento = complemento;
    this.objModel.localizacao = localizacao;
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
