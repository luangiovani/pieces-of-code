import { Component, OnInit, ChangeDetectorRef, ElementRef } from '@angular/core';
import { DTables } from 'src/app/_helpers/_dTables';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import Swal from 'sweetalert2';
import { TipoRecomendacaoService } from './tipo-recomendacao.service';
import { AuthenticationService } from 'src/app/_services/authentication.service';

declare function initApp(): any;

@Component({
  selector: 'app-tipo-recomendacao',
  templateUrl: './tipo-recomendacao.component.html'
})

  export class TipoRecomendacaoComponent implements OnInit {

    dataSource: any[] = [];
    title: string;

    constructor(public elementRef: ElementRef,
                public chRef: ChangeDetectorRef,
                public router: Router,
                public location: Location,
                public authService: AuthenticationService,
                public dTb: DTables,
                public service: TipoRecomendacaoService) {

        this.title = 'Tipos de Recomendações';
        initApp();
     }

    ngOnInit() {

      try {
        this.service.ObterTiposRecomendacoes()
          .subscribe((resp) => {
            if (resp.success) {
              this.dataSource = resp.results;
              this.chRef.detectChanges();
              this.dTb.montaTable(this.dataSource);
              this.chRef.detectChanges();
              this.dTb.adiciona_botoes_header();
              this.chRef.detectChanges();
              this.dTb.ajusta_toolbar('tipo-recomendacao-novo/', this.elementRef, this.router, this.location);
              this.chRef.detectChanges();
              this.dTb.ajusta_filter();
              this.chRef.detectChanges();
              this.dTb.ajusta_label();
            } else {
              Swal.fire({
                type: 'error',
                title: 'Oops...',
                text: 'Houve um erro ao tentar Obter os Tipos de Recomendações cadastrados!'
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
        html: 'Deseja ' + (bAtivo ? 'ativar' : 'inativar' ) + ' o tipo de recomendação <b>' + nome + '</b>?',
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
                  html: 'Tipo de Recomendação gravado no banco de dados',
                  type: 'success',
                  confirmButtonText: 'OK'
                })
                .then((res) => {
                  window.location.href = 'tipo-recomendacao';
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
      this.router.navigate(['tipo-recomendacao-novo/', id]);
    }
  }
