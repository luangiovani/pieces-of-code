import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { IGenericoListResponse, IGenericoResponse } from 'src/app/models/GenericoResponseModel';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class ProdutosService {

  constructor(private http: HttpClient) { }
  objModel: any;
  sUrl = environment.baseUrl + environment.controllers.admin.produtos;
  sUrlListar = this.sUrl + environment.routes.common.listar;
  sUrlConsultar = this.sUrl + environment.routes.common.consultar;
  sUrlGravar = this.sUrl + environment.routes.common.gravar;
  sUrlAtivarInativar = this.sUrl + environment.routes.common.situacao;

  ObterProdutos() {
    return this.http.get<IGenericoListResponse>(this.sUrlListar);
  }

  async ObterOpcoesValores() {
    const opValores = this.http.get<IGenericoListResponse>(this.sUrl + 'opcoes-valores');
    return opValores.toPromise()
  }

  async ObterProduto(id: string = '') {
    const prod = this.http.get<IGenericoResponse>(this.sUrlConsultar + '?id=' + id);
    return prod.toPromise();
  }

  ConfirmarProduto(id: string,
                   ativo: string,
                   b64Imagem: string,
                   disponivel: string,
                   dataDisponibilidade: string,
                   valorPontos: string,
                   valorMonetario: string,
                   nome: string,
                   descricao: string,
                   observacoes: string,
                   colaborador: string): Observable<IGenericoResponse> {

    this.objModel = new Object();
    this.objModel.id = id;
    this.objModel.loja_id = '';
    this.objModel.nome = nome;
    this.objModel.descricao = descricao;
    this.objModel.b64_imagem = b64Imagem;
    this.objModel.disponibilidade = (disponivel === '' ? 'true' : (disponivel === 'Sim') ? 'true' : 'false');
    this.objModel.data_disponibilidade = dataDisponibilidade;
    this.objModel.valor_pontos = valorPontos;
    this.objModel.valor_monetario = valorMonetario;
    this.objModel.observacao = observacoes;
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
