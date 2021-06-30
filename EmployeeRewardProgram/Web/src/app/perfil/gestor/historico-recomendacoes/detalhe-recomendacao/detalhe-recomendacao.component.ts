import { Component, OnInit, ElementRef, ChangeDetectorRef } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { HistoricoRecomendacoesService } from '../historico-recomendacoes.service';
import 'datatables.net';
import 'datatables.net-bs4';
import Swal from 'sweetalert2';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { DTables } from 'src/app/_helpers/_dTables';
declare function initApp(): any;

@Component({
  selector: 'app-detalhe-recomendacao',
  templateUrl: './detalhe-recomendacao.component.html'
})

export class DetalheRecomendacaoComponent implements OnInit {

  recomendacaoId: string;
  aplicacaoForm: FormGroup;
  isLoading = false;
  objRecomendacao: any;
  title: string;

  constructor(public elementRef: ElementRef,
              public chRef: ChangeDetectorRef,
              public router: Router,
              public location: Location,
              public dTb: DTables,
              public authService: AuthenticationService,
              public service: HistoricoRecomendacoesService,
              public fb: FormBuilder,
              public activatedroute: ActivatedRoute) {

    activatedroute.params.subscribe(params => { this.recomendacaoId = params.id; });
    this.title = 'Recomendação # ';
    this.objRecomendacao = new Object();
    initApp();
  }

  async ngOnInit() {
    try {
      if (this.recomendacaoId !== '' && this.recomendacaoId !== undefined && this.recomendacaoId !== null) {
        await this.service.ObterDetalheRecomendacao(this.recomendacaoId)
          .then((resp) => {
              if (resp.success) {
                this.objRecomendacao = resp.obj;
                this.title += this.objRecomendacao.sequencial;
                this.title += ' - Solicitante.: ' +
                  (this.objRecomendacao.cs_gestor_solicitante + ' ' + this.objRecomendacao.gestor_solicitante);
                this.chRef.detectChanges();
                this.dTb.montaTable(this.objRecomendacao.avaliacoes);
                this.chRef.detectChanges();
                // this.dTb.adiciona_botoes_header();
                // this.chRef.detectChanges();
                // this.dTb.ajusta_toolbar('reconhecer/', this.elementRef, this.router, this.location);
                // this.chRef.detectChanges();
                this.dTb.ajusta_filter();
                // this.chRef.detectChanges();
                this.dTb.ajusta_label();
              } else {
                this.title = 'Status da Recomendação';
                this.objRecomendacao.id = '';
                this.objRecomendacao.sequencial = 0;
                this.objRecomendacao.colaborador = '';
                this.objRecomendacao.cs_colaborador = '';
                this.objRecomendacao.cs_gestor_colaborador = '';
                this.objRecomendacao.gestor_colaborador = '';
                this.objRecomendacao.cs_gestor_solicitante = '';
                this.objRecomendacao.gestor_solicitante = '';
                this.objRecomendacao.status = '';
                this.objRecomendacao.tipo_recomendacao = '';
                this.objRecomendacao.qtde_pontos = '';
                this.objRecomendacao.justificativa = '';
              }
          });
      } else {
        this.title = 'Status da Recomendação';
        this.objRecomendacao.id = '';
        this.objRecomendacao.sequencial = 0;
        this.objRecomendacao.colaborador = '';
        this.objRecomendacao.cs_colaborador = '';
        this.objRecomendacao.cs_gestor_colaborador = '';
        this.objRecomendacao.gestor_colaborador = '';
        this.objRecomendacao.cs_gestor_solicitante = '';
        this.objRecomendacao.gestor_solicitante = '';
        this.objRecomendacao.status = '';
        this.objRecomendacao.tipo_recomendacao = '';
        this.objRecomendacao.qtde_pontos = '';
        this.objRecomendacao.justificativa = '';
      }
    } catch (err) {
      Swal.fire({
        title: 'Operação não realizada!',
        html: err.message,
        type: 'error',
        confirmButtonText: 'OK'
      });
    }
  }

  // confirmar(): void {

  //   Swal.fire({
  //     title: 'Confirmar Operação',
  //     html: 'Confirmar ' + this.title + '?',
  //     type: 'question',
  //     showCancelButton: true,
  //     confirmButtonText: 'Confirmar',
  //     cancelButtonText: 'Cancelar'
  //   })
  //   .then((result) => {
  //     if (result.value) {
  //       this.service.ConfirmarAplicacao(
  //         this.aplicacaoForm.controls.id.value,
  //         this.aplicacaoForm.controls.descricao.value,
  //         this.aplicacaoForm.controls.ativo.value,
  //         this.currentUser.cs)
  //         .subscribe((recModel) => {
  //           if (recModel.success) {
  //             Swal.fire({
  //               title: 'Operação realizada!',
  //               html: 'Aplicação gravada no banco de dados',
  //               type: 'success',
  //               confirmButtonText: 'OK'
  //             })
  //             .then((res) => {
  //               this.router.navigate(['aplicacao']);
  //               // window.location.href = 'aplicacao';
  //             });
  //           } else {
  //             Swal.fire({
  //               title: 'Operação não realizada!',
  //               html: recModel.message,
  //               type: 'error',
  //               confirmButtonText: 'OK'
  //             });
  //           }
  //         });
  //     }
  //   });
  // }

  goback() {
    this.location.back();
  }

}
