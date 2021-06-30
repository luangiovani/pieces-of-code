import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { IGenericoListResponse, IGenericoResponse } from 'src/app/models/GenericoResponseModel';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TaxaConversaoService {

  constructor(private http: HttpClient) { }

  objModel: any;
  sUrl = environment.baseUrl + environment.controllers.admin.taxaconversao;
  sUrlListar = this.sUrl + environment.routes.common.listar;
  sUrlConsultar = this.sUrl + environment.routes.common.consultar;
  sUrlGravar = this.sUrl + environment.routes.common.gravar;
  sUrlAtivarInativar = this.sUrl + environment.routes.common.situacao;

  async ObterAtual() {
    const taxas = await this.http.get<IGenericoResponse>(this.sUrl + 'obter-atual');
    return taxas.toPromise();
  }

  async ObterTaxa(id: string) {
    return this.http.get<IGenericoResponse>(this.sUrlConsultar + '?id=' + id).toPromise();
  }

  ObterTaxas() {
    return this.http.get<IGenericoListResponse>(this.sUrlListar);
  }

  ConfirmarTaxa(id: string,
                ativo: string,
                valorMoeda: string,
                fator: number,
                nome: string,
                colaborador: string): Observable<IGenericoResponse> {

  this.objModel = new Object();
  this.objModel.id = id;
  this.objModel.ativo = (id === '' ? 'true' : ativo);
  this.objModel.nome = nome;
  valorMoeda = valorMoeda.replace('.', '');
  valorMoeda = valorMoeda.replace(',', '.');
  this.objModel.valor_moeda = parseFloat(valorMoeda);
  this.objModel.fator = fator;
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
