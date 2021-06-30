import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IAuthResponse } from '../models/UsuarioModel';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    private currentUserSubject: BehaviorSubject<any>;
    public currentUser: Observable<any>;

    constructor(private http: HttpClient) {
        this.currentUserSubject = new BehaviorSubject<any>(JSON.parse(sessionStorage.getItem('currentUser')));
        this.currentUser = this.currentUserSubject.asObservable();
    }

    public get currentUserValue() {
        return this.currentUserSubject.value;
    }

    login(username, password) {
        return this.http.post<IAuthResponse>(`${ environment.baseUrl }auth/login`,
        { login: username, senha: password, aplicacao_id: environment.aplicacao_id })
            .pipe(map(resp => {
                if (resp.success) {
                    // Armazena os detalhes do usuário logado, com seus menus e Token
                    sessionStorage.setItem('currentUser', JSON.stringify(resp.obj));
                    this.currentUserSubject.next(resp.obj);
                }
                return resp;
            }));
    }

    logout() {
        sessionStorage.removeItem('currentUser');
        this.currentUserSubject.next(null);
    }

    async getSaldoPontosGestor() {
        return this.http.get<IAuthResponse>(`${ environment.baseUrl }colaborador/obter-saldo-pontos`).toPromise();
    }
}
