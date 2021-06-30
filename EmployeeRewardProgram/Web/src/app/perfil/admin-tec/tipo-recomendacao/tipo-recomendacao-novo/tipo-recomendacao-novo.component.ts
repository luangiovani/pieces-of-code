import { Component, OnInit, ElementRef, ChangeDetectorRef } from '@angular/core';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { TipoRecomendacaoService } from '../tipo-recomendacao.service';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import Swal from 'sweetalert2';
declare function initApp(): any;

@Component({
  selector: 'app-tipo-recomendacao-novo',
  templateUrl: './tipo-recomendacao-novo.component.html'
})

export class TipoRecomendacaoNovoComponent implements OnInit {

  tipoRecomendacaoId: string;
  tipoRecomendacaoForm: FormGroup;
  isLoading = false;
  objTipoRecomendacao: any;
  title: string;
  currentUser: any;

  constructor(public elementRef: ElementRef,
              public chRef: ChangeDetectorRef,
              public router: Router,
              public location: Location,
              public authService: AuthenticationService,
              public service: TipoRecomendacaoService,
              public fb: FormBuilder,
              public activatedroute: ActivatedRoute) {

    activatedroute.params.subscribe(params => { this.tipoRecomendacaoId = params.tipoRecomendacaoId; });
    if (this.tipoRecomendacaoId === '' || this.tipoRecomendacaoId === undefined || this.tipoRecomendacaoId === null) {
      this.tipoRecomendacaoId = '';
      this.title = 'Cadastro de Tipo de Recomendação';
    } else {
      this.title = 'Edição de Tipo de Recomendação';
    }
    this.authService.currentUser.subscribe(x => this.currentUser = x);
    this.objTipoRecomendacao = new Object();
    initApp();
  }

  ngOnInit() {
    this.tipoRecomendacaoForm = this.fb.group({
      id: new FormControl(''),
      ativo: new FormControl(''),
      nome: new FormControl('', [Validators.required]),
      descricao: new FormControl(''),
      tipo_pontuacao: new FormControl(''),
      pontos_fixos: new FormControl('')
    });

    if (this.tipoRecomendacaoId !== '' && this.tipoRecomendacaoId !== undefined && this.tipoRecomendacaoId !== null) {
      this.service.ObterTipoRecomendacao(this.tipoRecomendacaoId)
        .subscribe((resp) => {
            if (resp.success) {
              this.objTipoRecomendacao = resp.obj;
              this.tipoRecomendacaoForm.controls.id.setValue(this.objTipoRecomendacao.id);
              this.tipoRecomendacaoForm.controls.ativo.setValue(this.objTipoRecomendacao.ativo);
              this.tipoRecomendacaoForm.controls.nome.setValue(this.objTipoRecomendacao.nome);
              this.tipoRecomendacaoForm.controls.descricao.setValue(this.objTipoRecomendacao.descricao);
              this.tipoRecomendacaoForm.controls.tipo_pontuacao.setValue(this.objTipoRecomendacao.tipo_pontuacao);
              this.tipoRecomendacaoForm.controls.pontos_fixos.setValue(this.objTipoRecomendacao.pontos_fixos);
            } else {
              this.objTipoRecomendacao.id = '';
              this.objTipoRecomendacao.ativo = 'true';
              this.objTipoRecomendacao.nome = '';
              this.objTipoRecomendacao.descricao = '';
              this.objTipoRecomendacao.tipo_pontuacao = 0;
              this.objTipoRecomendacao.pontos_fixos = 0;
            }
        });
    } else {
        this.objTipoRecomendacao.id = '';
        this.objTipoRecomendacao.ativo = 'true';
        this.objTipoRecomendacao.nome = '';
        this.objTipoRecomendacao.descricao = '';
        this.objTipoRecomendacao.tipo_pontuacao = 0;
        this.objTipoRecomendacao.pontos_fixos = 0;
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
        this.service.ConfirmarTipoRecomendacao(
          this.tipoRecomendacaoForm.controls.id.value,
          this.tipoRecomendacaoForm.controls.nome.value,
          this.tipoRecomendacaoForm.controls.descricao.value,
          this.tipoRecomendacaoForm.controls.tipo_pontuacao.value,
          this.tipoRecomendacaoForm.controls.pontos_fixos.value,
          this.tipoRecomendacaoForm.controls.ativo.value,
          this.currentUser.cs)
          .subscribe((recModel) => {
            if (recModel.success) {
              Swal.fire({
                title: 'Operação realizada!',
                html: 'Tipo de Recomendação gravada no banco de dados',
                type: 'success',
                confirmButtonText: 'OK'
              })
              .then((res) => {
                this.router.navigate(['tipo-recomendacao']);
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
