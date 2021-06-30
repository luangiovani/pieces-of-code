import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { IGenericoListResponse, IGenericoResponse } from 'src/app/models/GenericoResponseModel';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TrocarPontosService {

  constructor(private http: HttpClient) { }

  objModel: any;
  sUrl = environment.baseUrl + environment.controllers.colaborador.compra;
  sUrlProdutos = environment.baseUrl + environment.controllers.admin.produtos;
  sUrlListar = this.sUrl + environment.routes.common.listar;
  sUrlConsultar = this.sUrl + environment.routes.common.consultar;
  sUrlGravar = this.sUrl + environment.routes.common.gravar;
  sUrlAtivarInativar = this.sUrl + environment.routes.common.situacao;

  ObterProdutos() {
    return this.http.get<IGenericoListResponse>(this.sUrlProdutos + 'listar');
  }

  ConfirmarSolicitacaoTroca(qtde: number = 0,
                            opcaoEntregaId: string = '',
                            lojaId: string = '',
                            observacoes: string = '',
                            produtoId: string = ''): Observable<IGenericoResponse> {

    this.objModel = new Object();
    this.objModel.qtde = qtde;
    this.objModel.opcao_entrega_id = opcaoEntregaId;
    this.objModel.loja_id = lojaId;
    this.objModel.observacoes = observacoes;
    this.objModel.produto_id = produtoId;

    return this.http.post<IGenericoResponse>(this.sUrl + 'solicitar-troca-pontos', this.objModel);
  }
}
