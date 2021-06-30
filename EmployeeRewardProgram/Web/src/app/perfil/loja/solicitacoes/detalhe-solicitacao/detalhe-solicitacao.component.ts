import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { environment } from 'src/environments/environment';
import { Location } from '@angular/common';
import { AuthenticationService } from 'src/app/_services';
import { SolicitacoesService } from '../solicitacoes.service';
import { SituacaoCompraService } from 'src/app/perfil/admin-tec/situacao-compra/situacao-compra.service';
import Swal from 'sweetalert2';
declare function initApp(): any;

@Component({
  selector: 'app-detalhe-solicitacao',
  templateUrl: './detalhe-solicitacao.component.html',
  styleUrls: ['./detalhe-solicitacao.component.css']
})
export class DetalheSolicitacaoComponent implements OnInit {

  enums: any;
  title = '';
  compraId: '';
  objCompra: any;
  situacoesCompra: any[] = [];

  constructor(public chRef: ChangeDetectorRef,
              public router: Router,
              public location: Location,
              public authService: AuthenticationService,
              public service: SolicitacoesService,
              public situacoesService: SituacaoCompraService,
              public activatedroute: ActivatedRoute) {
                activatedroute.params.subscribe(params => { this.compraId = params.id; });
                this.title = 'Detalhe da Solicitação #';
                initApp();
                this.enums = environment.enumns;
              }

  async ngOnInit() {
    try {
      await this.situacoesService.ObterSituacoesCompraAsync()
        .then((resp) => {
          if (resp.success) {
            this.situacoesCompra = resp.results;
            try {
              this.service.ObterSolicitacao(this.compraId)
              .subscribe((solic) => {
                if (solic.success) {
                  this.objCompra = solic.obj;
                  this.title += solic.obj.sequencial;
                } else {
                  Swal.fire({
                    type: 'error',
                    title: 'Oops...',
                    text: 'Houve um erro ao tentar Obter o detalhe da Solicitação! ' + solic.message
                  });
                }
              });
            } catch (error) {
              Swal.fire({
                type: 'error',
                title: 'Oops...',
                text: 'Houve um erro ao tentar Obter o detalhe da Solicitação! ' + error.message
              });
            }
          }
        });
    } catch (error) {
      Swal.fire({
        type: 'error',
        title: 'Oops...',
        text: 'Houve um erro ao tentar Obter o detalhe da Solicitação! ' + error.message
      });
    }
  }

  efetivar() {
    Swal.fire({
      type: 'question',
      title: 'Confirmar',
      html: 'Deseja efetivar a Solicitação?',
      input: 'textarea',
      showConfirmButton: true,
      showCancelButton: true,
      confirmButtonText: 'Efetivar',
      cancelButtonText: 'Cancelar'
    }).then((res) => {
      this.service.AtualizarSolicitacao(this.compraId, environment.enumns.SituacaoCompraEnum.Efetivada, null, res.value)
          .subscribe((resp) => {
            if (resp.success) {
              Swal.fire({
                type: 'success',
                title: 'Operação realizada com Sucesso!',
                text: 'A compra foi Efetivada, proceda com a entrega ou envio dos produtos ao Colaborador!'
              }).then((r) => {
                this.router.navigate(['solicitacoes']);
              });
            } else {
              Swal.fire({
                type: 'error',
                title: 'Operação não realizada!',
                text: 'Ocorreu um erro ao tentar efetivar a solicitação! ' + resp.message
              });
            }
          });
    });
  }

  finalizar() {
    Swal.fire({
      type: 'question',
      title: 'Confirmar',
      html: 'Deseja finalizar a Solicitação?',
      showConfirmButton: true,
      showCancelButton: true,
      confirmButtonText: 'Finalizar',
      cancelButtonText: 'Cancelar'
    }).then((res) => {
      if (res.value) {
        this.service.AtualizarSolicitacao(this.compraId, environment.enumns.SituacaoCompraEnum.Finalizada)
          .subscribe((resp) => {
            if (resp.success) {
              Swal.fire({
                type: 'success',
                title: 'Operação realizada com Sucesso!',
                text: 'A compra foi finalizada!'
              }).then((r) => {
                this.router.navigate(['solicitacoes']);
              });
            } else {
              Swal.fire({
                type: 'error',
                title: 'Operação não realizada!',
                text: 'Ocorreu um erro ao tentar finalizar a solicitação! ' + resp.message
              });
            }
          });
      }
    });
  }

  recusar() {
    Swal.fire({
      type: 'question',
      title: 'Confirmar',
      html: 'Deseja recusar a Solicitação?',
      input: 'textarea',
      showConfirmButton: true,
      showCancelButton: true,
      confirmButtonText: 'Recusar',
      cancelButtonText: 'Cancelar'
    }).then((res) => {
      if (res.value) {
        this.service.AtualizarSolicitacao(this.compraId, environment.enumns.SituacaoCompraEnum.Recusada, res.value)
          .subscribe((resp) => {
            if (resp.success) {
              Swal.fire({
                type: 'success',
                title: 'Operação realizada com Sucesso!',
                text: 'A compra foi Recusada!'
              }).then((r) => {
                this.router.navigate(['solicitacoes']);
              });
            } else {
              Swal.fire({
                type: 'error',
                title: 'Operação não realizada!',
                text: 'Ocorreu um erro ao tentar recusar a solicitação! ' + resp.message
              });
            }
          });
      }
    });
  }

  goback() {
    this.location.back();
  }
}
