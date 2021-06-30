import { Component, OnInit, ChangeDetectorRef, ElementRef } from '@angular/core';
import { DTables } from 'src/app/_helpers/_dTables';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import Swal from 'sweetalert2';
import { AuthenticationService } from 'src/app/_services';
import { MeusPontosService } from './meus-pontos.service';
declare function initApp(): any;

@Component({
  selector: 'app-meus-pontos',
  templateUrl: './meus-pontos.component.html'
})

export class MeusPontosComponent implements OnInit {

  dataSource: any[] = [];
  title: string;
  currentUser: any;
  objSaldoPontos: any;

  constructor(public elementRef: ElementRef,
              public chRef: ChangeDetectorRef,
              public router: Router,
              public location: Location,
              public authService: AuthenticationService,
              public dTb: DTables,
              public service: MeusPontosService) {
              this.title = 'Meus Pontos';
              this.authService.currentUser.subscribe(x => this.currentUser = x);
              initApp();
   }

   async ngOnInit() {
    await this.authService.getSaldoPontosGestor()
    .then((resp) => {
      if (resp.success) {
        this.objSaldoPontos = resp.obj;
      } else {
        this.objSaldoPontos.qtde_pontos = 0;
        this.objSaldoPontos.qtde_verba = 0;
      }
    });

    this.chRef.detectChanges();
    this.title += '.: ' + this.objSaldoPontos.qtde_pontos.toFixed(0);

    try {
      this.service.ObterRelatorio(this.currentUser.cs)
        .subscribe((resp) => {
          if (resp.success) {
            this.dataSource = resp.results;
            this.chRef.detectChanges();
            this.dTb.montaTable(this.dataSource);
            this.dTb.ajusta_filter();
            this.dTb.ajusta_label();
          } else {
            Swal.fire({
              type: 'error',
              title: 'Oops...',
              text: 'Houve um erro ao tentar Obter Meu Extrato de Pontos!'
            });
          }
        });
    } catch (err) {
        Swal.fire({
          type: 'warning',
          title: 'Oops...',
          text: 'Ocorreu um erro interno.: ' + err.message
        });
    }
  }
}

