import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import Swal from 'sweetalert2';
import { AuthenticationService } from '../_services';
import { Router } from '@angular/router';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(private authenticationService: AuthenticationService,
                private router: Router) {}

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(catchError(err => {
            const error =
                (err.error !== undefined && err.error !== null ? err.error.message : err.message) 
                || err.statusText;
            if (err.status === 401) {
                Swal.fire({
                    title: 'Operação não realizada!',
                    html: 'Acesso não autorizado a este recurso!',
                    type: 'error',
                    confirmButtonText: 'OK'
                  }).then((r) => {
                    this.router.navigate(['/']);
                  });
            } else if (err.status !== 200) {
                Swal.fire({
                    title: 'Operação não realizada!',
                    html: 'Houve um erro ao tentar processar sua solicitação! ' + error,
                    type: 'error',
                    confirmButtonText: 'OK'
                  });
            }
            return throwError(error);
        }));
    }
}
