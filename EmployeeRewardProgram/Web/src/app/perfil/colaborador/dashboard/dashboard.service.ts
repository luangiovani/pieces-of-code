import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { IGenericoListResponse, IGenericoResponse } from 'src/app/models/GenericoResponseModel';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {

  constructor(private http: HttpClient) { }
  // Controller de Colaboradores
  sUrl = environment.baseUrl + environment.controllers.admin.colaboradores;

  ObterRelatorio(cs: string = '') {
    return this.http.get<IGenericoResponse>(this.sUrl + 'dashboard');
  }
}
