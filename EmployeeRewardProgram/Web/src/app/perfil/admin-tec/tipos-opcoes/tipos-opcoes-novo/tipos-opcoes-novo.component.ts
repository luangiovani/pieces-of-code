import { Component, OnInit, ElementRef, ChangeDetectorRef } from '@angular/core';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { TiposOpcoesService } from '../tipos-opcoes.service';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import Swal from 'sweetalert2';
declare function initApp(): any;

@Component({
  selector: 'app-tipos-opcoes-novo',
  templateUrl: './tipos-opcoes-novo.component.html'
})

export class TiposOpcoesNovoComponent implements OnInit {

  tipoOpcaoId: string;
  tipoOpcaoForm: FormGroup;
  isLoading = false;
  objTipoOpcao: any;
  title: string;
  currentUser: any;
  
  constructor(public elementRef: ElementRef,
              public chRef: ChangeDetectorRef,
              public router: Router,
              public location: Location,
              public authService: AuthenticationService,
              public service: TiposOpcoesService,
              public fb: FormBuilder,
              public activatedroute: ActivatedRoute) {

    activatedroute.params.subscribe(params => { this.tipoOpcaoId = params.tipoOpcaoId; });
    if (this.tipoOpcaoId === '' || this.tipoOpcaoId === undefined || this.tipoOpcaoId === null) {
      this.tipoOpcaoId = '';
      this.title = 'Cadastro de Tipo de Opção';
    } else {
      this.title = 'Edição de Tipo de Opção';
    }
    this.authService.currentUser.subscribe(x => this.currentUser = x);
    this.objTipoOpcao = new Object();
    initApp();
  }

  ngOnInit() {

    this.tipoOpcaoForm = this.fb.group({
      id: new FormControl(''),
      ativo: new FormControl(''),
      nome: new FormControl('', [Validators.required]),
      descricao: new FormControl('', [Validators.required])
    });

    if (this.tipoOpcaoId !== '' && this.tipoOpcaoId !== undefined && this.tipoOpcaoId !== null) {
      this.service.ObterTipoOpcao(this.tipoOpcaoId)
        .subscribe((resp) => {
            if (resp.success) {
              this.objTipoOpcao = resp.obj;
              this.tipoOpcaoForm.controls.id.setValue(this.objTipoOpcao.id);
              this.tipoOpcaoForm.controls.ativo.setValue(this.objTipoOpcao.ativo);
              this.tipoOpcaoForm.controls.nome.setValue(this.objTipoOpcao.nome);
              this.tipoOpcaoForm.controls.descricao.setValue(this.objTipoOpcao.descricao);
            } else {
              this.objTipoOpcao.id = '';
              this.objTipoOpcao.ativo = 'true';
              this.objTipoOpcao.nome = '';
              this.objTipoOpcao.descricao = '';
            }
        });
    } else {
        this.objTipoOpcao.id = '';
        this.objTipoOpcao.ativo = 'true';
        this.objTipoOpcao.nome = '';
        this.objTipoOpcao.descricao = '';
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
        this.service.ConfirmarTipoOpcao(
          this.tipoOpcaoForm.controls.id.value,
          this.tipoOpcaoForm.controls.nome.value,
          this.tipoOpcaoForm.controls.descricao.value,
          this.tipoOpcaoForm.controls.ativo.value,
          this.currentUser.cs)
          .subscribe((recModel) => {
            if (recModel.success) {
              Swal.fire({
                title: 'Operação realizada!',
                html: 'Tipo de Opção gravado no banco de dados',
                type: 'success',
                confirmButtonText: 'OK'
              })
              .then((res) => {
                this.router.navigate(['tipos-opcoes']);
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
