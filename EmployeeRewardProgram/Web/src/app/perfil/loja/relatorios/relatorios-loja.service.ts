import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { IGenericoListResponse, IGenericoResponse } from 'src/app/models/GenericoResponseModel';

@Injectable({
  providedIn: 'root'
})
export class RelatoriosLojaService {

  objModel: any;
  sUrl = environment.baseUrl + environment.controllers.colaborador.compra;
  sUrlProdutos = environment.baseUrl + environment.controllers.admin.produtos;
  sUrlColaborador = environment.baseUrl + environment.controllers.admin.colaboradores;
  sUrlListar = this.sUrl + environment.routes.common.listar;
  sUrlConsultar = this.sUrl + environment.routes.common.consultar;
  sUrlGravar = this.sUrl + environment.routes.common.gravar;
  sUrlAtivarInativar = this.sUrl + environment.routes.common.situacao;

  constructor(private http: HttpClient) { }

  ObterRelatorioTrocas(dataDe: string = '',
                       dataAte: string = '',
                       situacao: string = '',
                       pago: string = '',
                       lojaId: string = '') {
    return this.http.get<IGenericoListResponse>(this.sUrl + 'relatorio-trocas-efetuadas?dataDe=' + dataDe +
    '&dataAte=' + dataAte +
    '&situacao=' + situacao +
    '&pago=' + (pago === 'PG' ? '1' : (pago === 'NP' ? '0' : (pago === 'NC' ? '-1' : ''))) +
    '&loja_id=' + lojaId);
  }

  EnviarComprasFaturamento(comprasId: any[] = []) {
    return this.http.post<IGenericoResponse>(this.sUrl + 'faturar-compras', comprasId);
  }

  EfetuarPagamentoDeComprasFauradas(faturamentosId: any[] = []) {
    return this.http.post<IGenericoResponse>(this.sUrl + 'pagar-faturamentos', faturamentosId);
  }
}
