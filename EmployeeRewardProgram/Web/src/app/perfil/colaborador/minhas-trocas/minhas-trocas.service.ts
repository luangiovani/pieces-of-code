import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { IGenericoResponse, IGenericoListResponse } from 'src/app/models/GenericoResponseModel';

@Injectable({
  providedIn: 'root'
})
export class MinhasTrocasService {

  constructor(private http: HttpClient) { }

  sUrl = environment.baseUrl + environment.controllers.admin.colaboradores;
  sUrlCompra = environment.baseUrl + environment.controllers.colaborador.compra;

  ObterRelatorio() {
    return this.http.get<IGenericoListResponse>(this.sUrl + 'minhas-trocas');
  }

  Cancelar(id: string = '') {
    return this.http.get<IGenericoResponse>(this.sUrlCompra + 'cancelar-solicitacao-troca?id=' + id);
  }

  Receber(id: string = '') {
    return this.http.get<IGenericoResponse>(this.sUrlCompra + 'receber-produtos-troca?id=' + id);
  }
}
