import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { IGenericoListResponse, IGenericoResponse } from 'src/app/models/GenericoResponseModel';

@Injectable({
  providedIn: 'root'
})
export class ColaboradorService {

  constructor(private http: HttpClient) { }
  objModel: any;
  sUrl = environment.baseUrl + environment.controllers.admin.colaboradores;
  sUrlListar = this.sUrl + environment.routes.common.listar;
  sUrlConsultar = this.sUrl + environment.routes.common.consultar;
  sUrlGravar = this.sUrl + environment.routes.common.gravar;
  sUrlAtivarInativar = this.sUrl + environment.routes.common.situacao;

  /**
   *  Listar os Colaboradores do Gestor ou de outra equipe, com ou sem trocas de produtos
   *  csGestor.: Código do Gestor Logado
   *  meuTime.: True se for para obter colaboradores de sua equipe false para outra equipe
   *  comTrocas: True para listar somente colaboradores que já efeturam trocas
   */
  async ListarColaboradores(csGestor: string, meuTime: boolean = true, comTrocas: boolean = false) {
    const sUrlListarColaboradores =
      this.sUrl + 'listar-colaboradores?gestor_id=' + csGestor
      + '&meuTime=' + (meuTime ? 'true' : 'false')
      + '&comTrocas=' + (comTrocas ? 'true' : 'false');
    return this.http.get<IGenericoListResponse>(sUrlListarColaboradores).toPromise();
  }

  async ObterColaboradoresGestores() {
    return this.http.get<IGenericoListResponse>(this.sUrl + 'gestores').toPromise();
  }

  ObterColaboradores() {
    return this.http.get<IGenericoListResponse>(this.sUrlListar);
  }

  ObterColaborador(id: string = '') {
    return this.http.get<IGenericoResponse>(this.sUrlConsultar + '?id=' + id);
  }

  async ObterColaboradorAsync(cs: string = '') {
    return this.http.get<IGenericoResponse>(this.sUrlConsultar + '?cs=' + cs).toPromise();
  }

  AtivarInativar(id: string, ativar: string) {

    this.objModel = new Object();
    this.objModel.id = id;
    this.objModel.ativar = ativar;
    return this.http.post<IGenericoResponse>(this.sUrlAtivarInativar, this.objModel);
  }

  MudarPerfil(id: string, perfilId: string, perfil: string) {
    this.objModel = new Object();
    this.objModel.colaborador_id = id;
    this.objModel.perfil_id = perfilId;
    this.objModel.perfil = perfil;
    return this.http.post<IGenericoResponse>(this.sUrl + 'atualizar-perfil', this.objModel);
  }

}
