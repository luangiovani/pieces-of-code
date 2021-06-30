import { Component, OnInit, ElementRef, ChangeDetectorRef } from '@angular/core';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { SituacaoAvaliacaoService } from '../situacao-avaliacao.service';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import Swal from 'sweetalert2';
declare function initApp(): any;

@Component({
  selector: 'app-situacao-avaliacao-novo',
  templateUrl: './situacao-avaliacao-novo.component.html'
})

export class SituacaoAvaliacaoNovoComponent implements OnInit {

  situacaoAvaliacaoId: string;
  situacaoAvaliacaoForm: FormGroup;
  isLoading = false;
  objSituacaoAvaliacao: any;
  title: string;
  currentUser: any;
  constructor(public elementRef: ElementRef,
              public chRef: ChangeDetectorRef,
              public router: Router,
              public location: Location,
              public authService: AuthenticationService,
              public service: SituacaoAvaliacaoService,
              public fb: FormBuilder,
              public activatedroute: ActivatedRoute) {
    activatedroute.params.subscribe(params => { this.situacaoAvaliacaoId = params.situacaoAvaliacaoId; });
    if (this.situacaoAvaliacaoId === '' || this.situacaoAvaliacaoId === undefined || this.situacaoAvaliacaoId === null) {
      this.situacaoAvaliacaoId = '';
      this.title = 'Cadastro de Situação de Avaliação';
    } else {
      this.title = 'Edição de Situação de Avaliação';
    }
    this.authService.currentUser.subscribe(x => this.currentUser = x);
    this.objSituacaoAvaliacao = new Object();
    initApp();
  }

  ngOnInit() {
    this.situacaoAvaliacaoForm = this.fb.group({
      id: new FormControl(''),
      ativo: new FormControl(''),
      nome: new FormControl('', [Validators.required]),
      descricao: new FormControl('', [Validators.required])
    });

    if (this.situacaoAvaliacaoId !== '' && this.situacaoAvaliacaoId !== undefined && this.situacaoAvaliacaoId !== null) {
      this.service.ObterSituacaoAvaliacao(this.situacaoAvaliacaoId)
        .subscribe((resp) => {
            if (resp.success) {
              this.objSituacaoAvaliacao = resp.obj;
              this.situacaoAvaliacaoForm.controls.id.setValue(this.objSituacaoAvaliacao.id);
              this.situacaoAvaliacaoForm.controls.ativo.setValue(this.objSituacaoAvaliacao.ativo);
              this.situacaoAvaliacaoForm.controls.nome.setValue(this.objSituacaoAvaliacao.nome);
              this.situacaoAvaliacaoForm.controls.descricao.setValue(this.objSituacaoAvaliacao.descricao);
            } else {
              this.objSituacaoAvaliacao.id = '';
              this.objSituacaoAvaliacao.ativo = 'true';
              this.objSituacaoAvaliacao.nome = '';
              this.objSituacaoAvaliacao.descricao = '';
            }
        });
    } else {
        this.objSituacaoAvaliacao.id = '';
        this.objSituacaoAvaliacao.ativo = 'true';
        this.objSituacaoAvaliacao.nome = '';
        this.objSituacaoAvaliacao.descricao = '';
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
        this.service.ConfirmarSituacaoAvaliacao(
          this.situacaoAvaliacaoForm.controls.id.value,
          this.situacaoAvaliacaoForm.controls.nome.value,
          this.situacaoAvaliacaoForm.controls.descricao.value,
          this.situacaoAvaliacaoForm.controls.ativo.value,
          this.currentUser.cs)
          .subscribe((recModel) => {
            if (recModel.success) {
              Swal.fire({
                title: 'Operação realizada!',
                html: 'Situação de Avaliação gravada no banco de dados',
                type: 'success',
                confirmButtonText: 'OK'
              })
              .then((res) => {
                this.router.navigate(['situacao-avaliacao']);
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
