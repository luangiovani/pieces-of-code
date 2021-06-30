import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { IGenericoResponse } from 'src/app/models/GenericoResponseModel';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RealizarVendaService {

  objModel: any;
  sUrl = environment.baseUrl + environment.controllers.colaborador.compra;
  sUrlProdutos = environment.baseUrl + environment.controllers.admin.produtos;
  sUrlColaborador = environment.baseUrl + environment.controllers.admin.colaboradores;
  sUrlListar = this.sUrl + environment.routes.common.listar;
  sUrlConsultar = this.sUrl + environment.routes.common.consultar;
  sUrlGravar = this.sUrl + environment.routes.common.gravar;
  sUrlAtivarInativar = this.sUrl + environment.routes.common.situacao;

  constructor(private http: HttpClient) { }

  ObterColaborador(cs: string) {
    return this.http.get<IGenericoResponse>(this.sUrlColaborador + 'consultar?cs=' + cs);
  }

  async ObterColaboradorLoja(cs: string) {
    return this.http.get<IGenericoResponse>(this.sUrlColaborador + 'consultar-colaborador-loja?cs=' + cs)
    .toPromise();
  }

  RealizarVenda(qtde: number = 0,
                opcaoEntregaId: string = '',
                lojaId: string = '',
                observacoes: string = '',
                produtoId: string = '',
                csColaborador: string = ''): Observable<IGenericoResponse> {

    this.objModel = new Object();
    this.objModel.qtde = qtde;
    this.objModel.opcao_entrega_id = opcaoEntregaId;
    this.objModel.loja_id = lojaId;
    this.objModel.observacoes = observacoes;
    this.objModel.produto_id = produtoId;
    this.objModel.cs_colaborador = csColaborador;
    // Meio de Compra = NA LOja
    this.objModel.meio_de_compra_id = '2D2F7B07-9B3E-4E16-A097-BABBDD47551A';
    return this.http.post<IGenericoResponse>(this.sUrl + 'solicitar-troca-pontos', this.objModel);
  }
}
