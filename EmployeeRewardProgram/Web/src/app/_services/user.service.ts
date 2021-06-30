import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({ providedIn: 'root' })
export class UserService {
    constructor(private http: HttpClient) { }

    getAll() {
        return this.http.get<any[]>(`${ environment.baseUrl }/users`);
    }

    register(user) {
        return this.http.post(`${ environment.baseUrl }/users/register`, user);
    }

    delete(id) {
        return this.http.delete(`${ environment.baseUrl }/users/${id}`);
    }
}
