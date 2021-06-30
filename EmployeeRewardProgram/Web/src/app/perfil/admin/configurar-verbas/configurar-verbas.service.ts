import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { IGenericoResponse } from 'src/app/models/GenericoResponseModel';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class ConfigurarVerbasService {

  constructor(private http: HttpClient) { }
  objModel: any;
  sUrl = environment.baseUrl + environment.controllers.admin.configurar;
  sUrlVerba = this.sUrl + 'obter-configuracao-verba';
  sUrlGravarVerba = this.sUrl + 'cadastrar-atualizar-verba';

  async ObterConfiguracaoVerba() {
    const config = this.http.get<IGenericoResponse>(this.sUrlVerba);
    return config.toPromise();
  }

  ConfirmarConfiguracaoVerba(id: string,
                             ativo: string,
                             pontosMinimos: number,
                             pontosColaborador: number,
                             pontosArea: number,
                             disponivelApartir: string,
                             bloquearApartir: string): Observable<IGenericoResponse> {

    this.objModel = new Object();
    this.objModel.id = id;
    this.objModel.ativo = (id === '' ? 'true' : ativo);
    this.objModel.pontos_minimos = pontosMinimos.toFixed(2);
    this.objModel.pontos_por_colaborador = pontosColaborador.toFixed(2);
    this.objModel.pontos_por_area = pontosArea.toFixed(2);
    this.objModel.dt_disponivel = disponivelApartir;
    this.objModel.dt_bloquear = bloquearApartir;

    return this.http.post<IGenericoResponse>(this.sUrlGravarVerba, this.objModel);
  }
}
