import { Injectable } from '@angular/core';
import { IGenericoListResponse } from 'src/app/models/GenericoResponseModel';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class QtdePontosService {

  constructor(private http: HttpClient) { }
  // Controller de Colaboradores
  sUrl = environment.baseUrl + environment.controllers.admin.colaboradores;

  ObterRelatorio() {
    return this.http.get<IGenericoListResponse>(this.sUrl + 'extrato-colaboradores-gestor');
  }
}
