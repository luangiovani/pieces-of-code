import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { IGenericoListResponse } from 'src/app/models/GenericoResponseModel';

@Injectable({
  providedIn: 'root'
})
export class MeusPontosService {

  constructor(private http: HttpClient) { }
  // Controller de Colaboradores
  sUrl = environment.baseUrl + environment.controllers.admin.colaboradores;

  ObterRelatorio(cs: string = '') {
    return this.http.get<IGenericoListResponse>(this.sUrl + 'recomendacoes-recebidas?cs_colaborador=' + cs);
  }
}
