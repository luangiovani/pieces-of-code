import { Component, OnInit, ElementRef, ChangeDetectorRef } from '@angular/core';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { LojasService } from '../lojas.service';
import Swal from 'sweetalert2';
import { AuthenticationService } from 'src/app/_services/authentication.service';
declare function initApp(): any;

@Component({
  selector: 'app-lojas-novo',
  templateUrl: './lojas-novo.component.html'
})

export class LojasNovoComponent implements OnInit {

  lojaId: string;
  lojasForm: FormGroup;
  isLoading = false;
  objLoja: any;
  title: string;
  currentUser: any;

  constructor(public elementRef: ElementRef,
              public chRef: ChangeDetectorRef,
              public router: Router,
              public location: Location,
              public authService: AuthenticationService,
              public service: LojasService,
              public fb: FormBuilder,
              public activatedroute: ActivatedRoute) {

    activatedroute.params.subscribe(params => { this.lojaId = params.lojaId; });
    if (this.lojaId === '' || this.lojaId === undefined || this.lojaId === null) {
      this.title = 'Cadastro de Nova Loja';
    } else {
      this.title = 'Edição de Loja';
    }
    this.authService.currentUser.subscribe(x => this.currentUser = x);
    this.objLoja = new Object();
    initApp();
  }

  ngOnInit() {
    this.lojasForm = this.fb.group({
      id: new FormControl(''),
      ativo: new FormControl(''),
      nome: new FormControl('', [Validators.required]),
      statusLoja: new FormControl(''),
      codigo: new FormControl(''),
      observacao: new FormControl(''),
      complemento: new FormControl(''),
      localizacao: new FormControl('', [Validators.required]),
    });

    if (this.lojaId !== '' && this.lojaId !== undefined && this.lojaId !== null) {
      this.service.ObterLoja(this.lojaId)
        .subscribe((resp) => {
            if (resp.success) {
              this.objLoja = resp.obj;
              this.lojasForm.controls.id.setValue(this.objLoja.id);
              this.lojasForm.controls.ativo.setValue(this.objLoja.ativo);
              this.lojasForm.controls.nome.setValue(this.objLoja.nome);
              this.lojasForm.controls.statusLoja.setValue(this.objLoja.status_loja);
              this.lojasForm.controls.codigo.setValue(this.objLoja.codigo);
              this.lojasForm.controls.observacao.setValue(this.objLoja.observacao);
              this.lojasForm.controls.complemento.setValue(this.objLoja.complemento);
              this.lojasForm.controls.localizacao.setValue(this.objLoja.localizacao);
            } else {
              this.objLoja.id = '';
              this.objLoja.ativo = 'true';
              this.objLoja.nome = '';
              this.objLoja.status_loja = '';
              this.objLoja.codigo = '';
              this.objLoja.observacao = '';
              this.objLoja.complemento = '';
              this.objLoja.localizacao = '';
            }
        });
    } else {
      this.objLoja.id = '';
      this.objLoja.ativo = 'true';
      this.objLoja.nome = '';
      this.objLoja.status_loja = '';
      this.objLoja.codigo = '';
      this.objLoja.observacao = '';
      this.objLoja.complemento = '';
      this.objLoja.localizacao = '';
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
        this.service.ConfirmarLoja(
          this.lojasForm.controls.id.value,
          this.lojasForm.controls.nome.value,
          this.lojasForm.controls.statusLoja.value,
          this.lojasForm.controls.codigo.value,
          this.lojasForm.controls.observacao.value,
          this.lojasForm.controls.complemento.value,
          this.lojasForm.controls.localizacao.value,
          this.lojasForm.controls.ativo.value,
          this.currentUser.cs)
          .subscribe((recModel) => {
            if (recModel.success) {
              Swal.fire({
                title: 'Operação realizada!',
                html: 'Loja gravada no banco de dados',
                type: 'success',
                confirmButtonText: 'OK'
              })
              .then((res) => {
                this.router.navigate(['lojas']);
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
