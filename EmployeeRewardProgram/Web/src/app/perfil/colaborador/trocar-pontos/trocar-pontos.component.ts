import { Component, OnInit, ChangeDetectorRef, ElementRef } from '@angular/core';
import { DTables } from 'src/app/_helpers/_dTables';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import Swal from 'sweetalert2';
import { AuthenticationService } from 'src/app/_services';
import { TrocarPontosService } from './trocar-pontos.service';
declare function initApp(): any;

@Component({
  selector: 'app-trocar-pontos',
  templateUrl: './trocar-pontos.component.html'
})

export class TrocarPontosComponent implements OnInit {

  dataSource: any[] = [];
  title: string;
  currentUser: any;
  objSaldoPontos: any;

  constructor(public chRef: ChangeDetectorRef,
              public router: Router,
              public location: Location,
              public dTb: DTables,
              public authService: AuthenticationService,
              public service: TrocarPontosService) {

      this.title = 'Produtos para Troca | Meu saldo de Pontos';
      this.authService.currentUser.subscribe(x => this.currentUser = x);

      this.objSaldoPontos = new Object();
      /// Quantidade de Pontos de Verba do Gestor
      this.objSaldoPontos.qtde_verba = 0;
      /// Quantidade de Pontos de Saldo do Colaborador
      this.objSaldoPontos.qtde_pontos = 0;
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

    this.title += '.: ' + this.objSaldoPontos.qtde_pontos.toFixed(0);

    try {
      this.service.ObterProdutos()
      .subscribe((resp) => {
        if (resp.success) {
          this.dataSource = resp.results;
          this.chRef.detectChanges();
          // this.dTb.montaTable(this.dataSource);
          // this.dTb.ajusta_filter();
          // this.dTb.ajusta_label();
        } else {
          Swal.fire({
            type: 'warning',
            title: 'Oops...',
            text: 'Ocorreu um erro interno.: ' + resp.message
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

  goToDetalhesRecomendacao(id: string = '') {
    this.router.navigate(['detalhe-recomendacao/' + id]);
  }

  goback() {
    this.location.back();
  }

  solicitar(id: string = '', produtoPontos: number = 0) {
    if (this.objSaldoPontos.qtde_pontos >= produtoPontos) {
      this.router.navigate(['ver-produto/' + id]);
    } else {
      Swal.fire({
        type: 'error',
        title: 'Oops...',
        text: 'O Produto não pode ser selecionado, saldo insuficiente de pontos'
      });
    }
  }
}
