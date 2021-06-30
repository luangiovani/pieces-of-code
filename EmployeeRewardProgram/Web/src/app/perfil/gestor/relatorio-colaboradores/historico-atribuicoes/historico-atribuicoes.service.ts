import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { IGenericoListResponse, IGenericoResponse } from 'src/app/models/GenericoResponseModel';

@Injectable({
  providedIn: 'root'
})
export class HistoricoAtribuicoesService {

  constructor(private http: HttpClient) { }
  // Controller de Colaboradores
  sUrl = environment.baseUrl + environment.controllers.admin.colaboradores;

  ObterRelatorio() {
    return this.http.get<IGenericoListResponse>(this.sUrl + 'relatorio-pontuacao');
  }
}
