import { Component, OnInit, ChangeDetectorRef, ElementRef } from '@angular/core';
import { DTables } from 'src/app/_helpers/_dTables';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import * as $ from 'jquery';
import Swal from 'sweetalert2';
import { AvaliarService } from './avaliar.service';
import { AuthenticationService } from 'src/app/_services';
declare function initApp(): any;

@Component({
  selector: 'app-avaliar',
  templateUrl: './avaliar.component.html'
})
export class AvaliarComponent implements OnInit {

  dataSource: any[] = [];
  title: string;
  currentUser: any;

  constructor(public elementRef: ElementRef,
              public chRef: ChangeDetectorRef,
              public router: Router,
              public location: Location,
              public authService: AuthenticationService,
              public dTb: DTables,
              public service: AvaliarService) {

      this.title = 'Recomendações para realizar Avaliação';
      this.authService.currentUser.subscribe(x => this.currentUser = x);
      initApp();
   }

  async ngOnInit() {
    try {
      this.service.ObterRecomendacoes(this.currentUser.cs)
      .subscribe((resp) => {
        if (resp.success) {
          this.dataSource = resp.results;
          this.chRef.detectChanges();
          this.dTb.montaTable(this.dataSource);
          this.dTb.ajusta_filter();
          this.dTb.ajusta_label();
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

  avaliar(id: string = '', cod: string = '', cs: string = '', colab: string = '') {
    this.chRef.detectChanges();
    Swal.fire({
      title: 'Avaliar Recomendação.: ' + cod ,
      html: '<span>Colaborador.: ' + cs + ' - ' + colab + ' </span></br></br>' +
      '<label class="mr-5">' +
      '	<input type="radio" name="aprovar-radio" class="aprovar-radio" value="A" checked>' +
      '	<span class="swal2-label">Aprovar</span>' +
      '</label>' +
      '<label>' +
      '	<input type="radio" name="aprovar-radio" class="aprovar-radio" value="R">' +
      '	<span class="swal2-label">Reprovar</span>' +
      '</label>' +
      '</br>' +
      '<textarea class="swal2-textarea" placeholder="Inserir uma Justificativa" style="display: flex;"></textarea>',
      showCloseButton: true,
      showConfirmButton: true,
      confirmButtonText: 'Gravar',
      showCancelButton: true,
      cancelButtonText: 'Cancelar',
      showLoaderOnConfirm: true,
      preConfirm: () => {
        this.chRef.detectChanges();

        const radios = $('.aprovar-radio')[0] as HTMLInputElement;
        const iptRadio = (radios ? (radios.checked ? 'A' : 'R') : '');
        const iptText = $('.swal2-textarea').length > 0 ? $('.swal2-textarea').val() : '';

        if (iptRadio === '' || iptRadio === null || iptRadio === undefined) {
          Swal.showValidationMessage('Selecione se vai Aprovar ou Reprovar esta Recomendação!');
          return;
        }
        if (iptRadio === 'R' && (iptText === '' || iptText === null || iptText === undefined)) {
          Swal.showValidationMessage('Informe a Justificativa para esta Recomendação!');
          return;
        }

        this.service.AvaliarRecomendacao(id, iptText.toString(), iptRadio.toString())
          .subscribe((resp) => {
            if (resp.success) {
              if (iptRadio === 'A') {
                Swal.fire(
                  'Aprovado!',
                  'A Recomendação foi Aprovada!',
                  'success'
                ).then((res) => {
                  window.location.reload();
                });
              } else {
                Swal.fire(
                  'Reprovado!',
                  'A Recomendação foi Reprovada!',
                  'success'
                ).then((res) => {
                  window.location.reload();
                });
              }
            } else {
              Swal.fire({
                title: 'Operação não realizada!',
                html: resp.message,
                type: 'error',
                confirmButtonText: 'OK'
              });
            }
          });
      }
    });
  }

  goToDetalhesRecomendacao(id: string = '') {
    this.router.navigate(['detalhe-recomendacao/' + id]);
  }
}
