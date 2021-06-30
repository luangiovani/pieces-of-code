import { Component, OnInit, ChangeDetectorRef, ElementRef } from '@angular/core';
import { DTables } from 'src/app/_helpers/_dTables';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import Swal from 'sweetalert2';
import { TipoRecomendacaoService } from '../../admin-tec/tipo-recomendacao/tipo-recomendacao.service';
import { ColaboradorService } from '../../admin/colaboradores/colaborador.service';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { ReconhecerService } from './reconhecer.service';
declare function initApp(): any;

@Component({
  selector: 'app-reconhecer',
  templateUrl: './reconhecer.component.html'
})

export class ReconhecerComponent implements OnInit {

  tipoRecomendacao: any[] = [];
  csColaborador = '';
  meuTime = true;
  objColaborador: any;
  reconhecerForm: FormGroup;
  objTipo: any;
  title: string;
  currentUser: any;
  objSaldoPontos: any;

  constructor(public elementRef: ElementRef,
              public chRef: ChangeDetectorRef,
              public router: Router,
              public location: Location,
              public authService: AuthenticationService,
              public dTb: DTables,
              public service: ReconhecerService,
              public colaboradorService: ColaboradorService,
              public tipoRecomendacaoService: TipoRecomendacaoService,
              public activatedroute: ActivatedRoute,
              public fb: FormBuilder) {

      activatedroute.params.subscribe(params => { this.csColaborador = params.cs; });
      activatedroute.params.subscribe(params => { this.meuTime = (params.meutime === 'true'); });

      this.title = 'Reconhecer o Colaborador ';
      this.objColaborador = new Object();
      this.authService.currentUser.subscribe(x => this.currentUser = x);
      this.objSaldoPontos = new Object();
      /// Quantidade de Pontos de Verba do Gestor
      this.objSaldoPontos.qtde_verba = 0;
      /// Quantidade de Pontos de Saldo do Colaborador
      this.objSaldoPontos.qtde_pontos = 0;
      initApp();
   }

   async ngOnInit() {
    this.reconhecerForm = this.fb.group({
      cs: new FormControl(''),
      nome: new FormControl(''),
      cargo: new FormControl(''),
      tipoRecomendacao: new FormControl('', [Validators.required]),
      pontosRecomendacao: new FormControl('', [Validators.required]),
      justificativa: new FormControl('')
    });

    await this.authService.getSaldoPontosGestor()
      .then((resp) => {
        if (resp.success) {
          this.objSaldoPontos = resp.obj;
        } else {
          this.objSaldoPontos.qtde_pontos = 0;
          this.objSaldoPontos.qtde_verba = 0;
        }
    });

    this.reconhecerForm.controls.cs.disable();
    this.reconhecerForm.controls.nome.disable();
    this.reconhecerForm.controls.cargo.disable();

    try {
      await this.tipoRecomendacaoService.ListarTiposRecomendacoes()
        .then((resp) => {
          if (resp.success) {
            this.tipoRecomendacao = resp.results;
          } else {
            Swal.fire({
              type: 'warning',
              title: 'Oops...',
              html: 'Ocorreu um erro interno.: ' + resp.message
            });
          }
      });

      await this.colaboradorService.ObterColaboradorAsync(this.csColaborador)
        .then((resp) => {
          if (resp.success) {
            this.objColaborador = resp.obj;
            this.reconhecerForm.controls.cs.setValue(this.objColaborador.cs);
            this.reconhecerForm.controls.nome.setValue(this.objColaborador.nome);
            this.reconhecerForm.controls.cargo.setValue(this.objColaborador.cargo);
          } else {
            Swal.fire({
              type: 'warning',
              title: 'Oops...',
              html: 'Ocorreu um erro interno.: ' + resp.message
            }).then((res) => {
              if (res) {
                if (this.meuTime) {
                  this.router.navigate(['reconhecer-equipe']);
                } else {
                  this.router.navigate(['reconhecer-outra-equipe']);
                }
              }
            });
            return;
          }
      });
    } catch (err) {
      Swal.fire({
        type: 'warning',
        title: 'Oops...',
        html: 'Ocorreu um erro interno.: ' + err.message
      });
    }

    if (this.meuTime) {
      this.title += 'da Minha Equipe';
    } else {
      this.title += 'de Outra Equipe';
    }
  }

  changeTipo() {
    this.chRef.detectChanges();
    try {
      if (this.reconhecerForm.controls.tipoRecomendacao.value !== '') {
        this.objTipo =
          this.tipoRecomendacao
            .filter(tp => (tp.id.toUpperCase() === this.reconhecerForm.controls.tipoRecomendacao.value))[0];
        if (this.objTipo.tipo_pontuacao === 2) {
          this.reconhecerForm.controls.pontosRecomendacao.setValue(this.objTipo.pontos_fixos);
          this.reconhecerForm.controls.pontosRecomendacao.disable();
        } else {
          this.reconhecerForm.controls.pontosRecomendacao.enable();
          this.reconhecerForm.controls.pontosRecomendacao.setValue('');
        }
      } else {
        this.reconhecerForm.controls.pontosRecomendacao.enable();
        this.reconhecerForm.controls.pontosRecomendacao.setValue('');
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

  confirmar(): void {

    if (this.objSaldoPontos.qtde_verba < this.reconhecerForm.controls.pontosRecomendacao.value) {
      Swal.fire({
        title: 'Operação não realizada!',
        html: 'Seu saldo de pontos em Verba é insuficiente para efetuar o reconhecimento!',
        type: 'error',
        confirmButtonText: 'OK'
      });
    } else {
      Swal.fire({
        title: 'Confirmar Operação',
        html: 'Confirmar ' + this.title + '?',
        type: 'question',
        showCancelButton: true,
        confirmButtonText: 'Confirmar',
        cancelButtonText: 'Cancelar'
      })
      .then((result) => {
        if (result.value) {
          this.service.ConfirmarRecomendacao(
            this.reconhecerForm.controls.cs.value,
            this.currentUser.cs,
            this.reconhecerForm.controls.tipoRecomendacao.value,
            this.reconhecerForm.controls.pontosRecomendacao.value,
            this.reconhecerForm.controls.justificativa.value)
            .subscribe((resp) => {
              if (resp.success) {
                Swal.fire({
                  title: 'Operação realizada!',
                  html: 'Reconhecimento realizado para o Colaborador',
                  type: 'success',
                  confirmButtonText: 'OK'
                })
                .then((res) => {
                  if (this.meuTime) {
                    this.router.navigate(['reconhecer-equipe']);
                  } else {
                    this.router.navigate(['reconhecer-outra-equipe']);
                  }
                });
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
  }

  goback() {
    this.location.back();
  }

}
