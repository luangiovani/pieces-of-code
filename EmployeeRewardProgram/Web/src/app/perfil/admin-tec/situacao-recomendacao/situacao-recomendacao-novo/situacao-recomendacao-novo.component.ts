import { Component, OnInit, ElementRef, ChangeDetectorRef } from '@angular/core';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { SituacaoRecomendacaoService } from '../situacao-recomendacao.service';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import Swal from 'sweetalert2';
declare function initApp(): any;

@Component({
  selector: 'app-situacao-recomendacao-novo',
  templateUrl: './situacao-recomendacao-novo.component.html'
})

export class SituacaoRecomendacaoNovoComponent implements OnInit {

  situacaoRecomendacaoId: string;
  situacaoRecomendacaoForm: FormGroup;
  isLoading = false;
  objSituacaoRecomendacao: any;
  title: string;
  currentUser: any;

  constructor(public elementRef: ElementRef,
              public chRef: ChangeDetectorRef,
              public router: Router,
              public location: Location,
              public authService: AuthenticationService,
              public service: SituacaoRecomendacaoService,
              public fb: FormBuilder,
              public activatedroute: ActivatedRoute) {

    activatedroute.params.subscribe(params => { this.situacaoRecomendacaoId = params.situacaoRecomendacaoId; });
    if (this.situacaoRecomendacaoId === '' || this.situacaoRecomendacaoId === undefined || this.situacaoRecomendacaoId === null) {
      this.situacaoRecomendacaoId = '';
      this.title = 'Cadastro de Situação de Recomendação';
    } else {
      this.title = 'Edição de Situação de Recomendação';
    }
    this.authService.currentUser.subscribe(x => this.currentUser = x);
    this.objSituacaoRecomendacao = new Object();
    initApp();
  }

  ngOnInit() {
    this.situacaoRecomendacaoForm = this.fb.group({
      id: new FormControl(''),
      ativo: new FormControl(''),
      nome: new FormControl('', [Validators.required]),
      descricao: new FormControl('', [Validators.required])
    });

    if (this.situacaoRecomendacaoId !== '' && this.situacaoRecomendacaoId !== undefined && this.situacaoRecomendacaoId !== null) {
      this.service.ObterSituacaoRecomendacao(this.situacaoRecomendacaoId)
        .subscribe((resp) => {
            if (resp.success) {
              this.objSituacaoRecomendacao = resp.obj;
              this.situacaoRecomendacaoForm.controls.id.setValue(this.objSituacaoRecomendacao.id);
              this.situacaoRecomendacaoForm.controls.ativo.setValue(this.objSituacaoRecomendacao.ativo);
              this.situacaoRecomendacaoForm.controls.nome.setValue(this.objSituacaoRecomendacao.nome);
              this.situacaoRecomendacaoForm.controls.descricao.setValue(this.objSituacaoRecomendacao.descricao);
            } else {
              this.objSituacaoRecomendacao.id = '';
              this.objSituacaoRecomendacao.ativo = 'true';
              this.objSituacaoRecomendacao.nome = '';
              this.objSituacaoRecomendacao.descricao = '';
            }
        });
    } else {
        this.objSituacaoRecomendacao.id = '';
        this.objSituacaoRecomendacao.ativo = 'true';
        this.objSituacaoRecomendacao.nome = '';
        this.objSituacaoRecomendacao.descricao = '';
    }
  }

  confirmar(): void {

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
        this.service.ConfirmarSituacaoRecomendacao(
          this.situacaoRecomendacaoForm.controls.id.value,
          this.situacaoRecomendacaoForm.controls.nome.value,
          this.situacaoRecomendacaoForm.controls.descricao.value,
          this.situacaoRecomendacaoForm.controls.ativo.value,
          this.currentUser.cs)
          .subscribe((recModel) => {
            if (recModel.success) {
              Swal.fire({
                title: 'Operação realizada!',
                html: 'Situação de Recomendação gravada no banco de dados',
                type: 'success',
                confirmButtonText: 'OK'
              })
              .then((res) => {
                this.router.navigate(['situacao-recomendacao']);
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
  
  goback() {
    this.location.back();
  }

}
