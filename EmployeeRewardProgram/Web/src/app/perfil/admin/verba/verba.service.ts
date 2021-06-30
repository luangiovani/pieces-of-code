import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { IGenericoListResponse, IGenericoResponse } from 'src/app/models/GenericoResponseModel';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class VerbaService {

  constructor(private http: HttpClient) { }
  objModel: any;
  sUrl = environment.baseUrl + environment.controllers.admin.verba;
  sUrlListar = this.sUrl + environment.routes.common.listar;
  sUrlConsultar = this.sUrl + environment.routes.common.consultar;
  sUrlGravar = this.sUrl + environment.routes.common.gravar;
  sUrlAtivarInativar = this.sUrl + environment.routes.common.situacao;

  ObterVerbas() {
    return this.http.get<IGenericoListResponse>(this.sUrlListar);
  }

  async ObterVerba(id: string = '') {
    return this.http.get<IGenericoResponse>(this.sUrlConsultar + '?id=' + id).toPromise();
  }

  ConfirmarVerba(id: string,
                 ativo: string,
                 csColaborador: string,
                 valorPontos: string,
                 valorMoeda: string,
                 observacoes: string,
                 colaborador: string): Observable<IGenericoResponse> {

    this.objModel = new Object();
    this.objModel.id = id;
    this.objModel.ativo = (id === '' ? 'true' : ativo);
    this.objModel.cs_colaborador = csColaborador;
    this.objModel.valor_pontos = parseFloat(valorPontos).toFixed(0);
    this.objModel.valor_moeda = parseFloat(valorMoeda).toFixed(2);
    this.objModel.observacao = observacoes;
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
