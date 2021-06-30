import { Component, OnInit, ElementRef, ChangeDetectorRef } from '@angular/core';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { AplicacaoService } from '../aplicacao.service';
import 'datatables.net';
import 'datatables.net-bs4';
import Swal from 'sweetalert2';
import { AuthenticationService } from 'src/app/_services/authentication.service';
declare function initApp(): any;

@Component({
  selector: 'app-aplicacao-novo',
  templateUrl: './aplicacao-novo.component.html'
})
export class AplicacaoNovoComponent implements OnInit {

  aplicacaoId: string;
  aplicacaoForm: FormGroup;
  isLoading = false;
  objAplicacao: any;
  title: string;
  currentUser: any;
  constructor(public elementRef: ElementRef,
              public chRef: ChangeDetectorRef,
              public router: Router,
              public location: Location,
              public authService: AuthenticationService,
              public service: AplicacaoService,
              public fb: FormBuilder,
              public activatedroute: ActivatedRoute) {
    activatedroute.params.subscribe(params => { this.aplicacaoId = params.aplicacaoId; });
    if (this.aplicacaoId === '' || this.aplicacaoId === undefined || this.aplicacaoId === null) {
      this.title = 'Cadastro de Nova Aplicação';
    } else {
      this.title = 'Edição de Aplicação';
    }
    this.authService.currentUser.subscribe(x => this.currentUser = x);
    this.objAplicacao = new Object();
    initApp();
  }

  ngOnInit() {
    this.aplicacaoForm = this.fb.group({
      id: new FormControl(''),
      ativo: new FormControl(''),
      descricao: new FormControl('', [Validators.required])
    });

    if (this.aplicacaoId !== '' && this.aplicacaoId !== undefined && this.aplicacaoId !== null) {
      this.service.ObterAplicacao(this.aplicacaoId)
        .subscribe((resp) => {
            if (resp.success) {
              this.objAplicacao = resp.obj;
              this.aplicacaoForm.controls.id.setValue(this.objAplicacao.id);
              this.aplicacaoForm.controls.descricao.setValue(this.objAplicacao.descricao);
              this.aplicacaoForm.controls.ativo.setValue(this.objAplicacao.ativo);
            } else {
              this.objAplicacao.ativo = 'true';
              this.objAplicacao.id = '';
              this.objAplicacao.descricao = '';
            }
        });
    } else {
      this.objAplicacao.ativo = 'true';
      this.objAplicacao.id = '';
      this.objAplicacao.descricao = '';
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
        this.service.ConfirmarAplicacao(
          this.aplicacaoForm.controls.id.value,
          this.aplicacaoForm.controls.descricao.value,
          this.aplicacaoForm.controls.ativo.value,
          this.currentUser.cs)
          .subscribe((recModel) => {
            if (recModel.success) {
              Swal.fire({
                title: 'Operação realizada!',
                html: 'Aplicação gravada no banco de dados',
                type: 'success',
                confirmButtonText: 'OK'
              })
              .then((res) => {
                this.router.navigate(['aplicacao']);
                // window.location.href = 'aplicacao';
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
