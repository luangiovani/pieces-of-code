import { Component, OnInit, ChangeDetectorRef, ElementRef } from '@angular/core';
import { DTables } from 'src/app/_helpers/_dTables';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import Swal from 'sweetalert2';
import { SituacaoCompraService } from './situacao-compra.service';
import { AuthenticationService } from 'src/app/_services/authentication.service';
declare function initApp(): any;

@Component({
  selector: 'app-situacao-compra',
  templateUrl: './situacao-compra.component.html'
})

export class SituacaoCompraComponent implements OnInit {

  dataSource: any[] = [];
  title: string;

  constructor(public elementRef: ElementRef,
              public chRef: ChangeDetectorRef,
              public router: Router,
              public location: Location,
              public authService: AuthenticationService,
              public dTb: DTables,
              public service: SituacaoCompraService) {
      this.title = 'Situações de Compra';
      initApp();
   }

  ngOnInit() {
    try {
      this.service.ObterSituacoesCompra()
        .subscribe((resp) => {
          if (resp.success) {
            this.dataSource = resp.results;
            this.chRef.detectChanges();
            this.dTb.montaTable(this.dataSource);
            this.chRef.detectChanges();
            this.dTb.adiciona_botoes_header();
            this.chRef.detectChanges();
            this.dTb.ajusta_toolbar('situacao-compra-novo/', this.elementRef, this.router, this.location);
            this.chRef.detectChanges();
            this.dTb.ajusta_filter();
            this.chRef.detectChanges();
            this.dTb.ajusta_label();
          } else {
            Swal.fire({
              type: 'error',
              title: 'Oops...',
              text: 'Houve um erro ao tentar Obter as Situações de Compra cadastradas!'
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

  situacao(id: string = '', nome: string = '', ativo: string = 'true') {
    const bAtivo = ativo === 'true';
    Swal.fire({
      title: 'Confirmar Operação',
      html: 'Deseja ' + (bAtivo ? 'ativar' : 'inativar' ) + ' a situação <b>' + nome + '</b>?',
      type: 'question',
      showCancelButton: true,
      confirmButtonText: 'Confirmar',
      cancelButtonText: 'Cancelar'
    })
    .then((result) => {
      if (result.value) {
        this.service.AtivarInativar(
          id,
          ativo)
          .subscribe((recModel) => {
            if (recModel.success) {
              Swal.fire({
                title: 'Operação realizada!',
                html: 'Situação de Compra gravada no banco de dados',
                type: 'success',
                confirmButtonText: 'OK'
              })
              .then((res) => {
                window.location.href = 'situacao-compra';
              });
            } else {
              Swal.fire({
                title: 'Operação não realizada!',
                html: recModel.message,
                type: 'error',
                confirmButtonText: 'OK'
              });
            }
          });
      }
    });
  }

  editar(id: string) {
    this.router.navigate(['situacao-compra-novo/', id]);
  }
}
