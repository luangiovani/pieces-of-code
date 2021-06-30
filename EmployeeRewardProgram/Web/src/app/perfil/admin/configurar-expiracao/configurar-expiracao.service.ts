import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { IGenericoResponse } from 'src/app/models/GenericoResponseModel';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class ConfigurarExpiracaoService {

  constructor(private http: HttpClient) { }
  objModel: any;
  sUrl = environment.baseUrl + environment.controllers.admin.configurar;
  sUrlExpiracao = this.sUrl + 'obter-configuracao-expiracao';
  sUrlGravarExpiracao = this.sUrl + 'cadastrar-atualizar-expiracao';

  async ObterConfiguracaoExpiracao() {
    const config = this.http.get<IGenericoResponse>(this.sUrlExpiracao);
    return config.toPromise();
  }

  ConfirmarExpiracaoPontos(id: string,
                           ativo: string,
                           qtdeExpiracao: number,
                           tipoExpiracao: string,
                           qtdeExpiracaoDesligamento: number,
                           tipoExpiracaoDesligamento: string): Observable<IGenericoResponse> {

    this.objModel = new Object();
    this.objModel.id = id;
    this.objModel.ativo = (id === '' ? 'true' : ativo);
    this.objModel.qtde_expiracao = qtdeExpiracao.toFixed(2);
    this.objModel.tipo_expiracao = tipoExpiracao;
    this.objModel.qtde_expiracao_desligamento = qtdeExpiracaoDesligamento.toFixed(2);
    this.objModel.tipo_expiracao_desligamento = tipoExpiracaoDesligamento;

    return this.http.post<IGenericoResponse>(this.sUrlGravarExpiracao, this.objModel);
  }
}
