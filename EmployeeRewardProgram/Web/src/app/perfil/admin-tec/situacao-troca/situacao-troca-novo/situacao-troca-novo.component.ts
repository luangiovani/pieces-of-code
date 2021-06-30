import { Component, OnInit, ElementRef, ChangeDetectorRef } from '@angular/core';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { SituacaoTrocaService } from '../situacao-troca.service';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import Swal from 'sweetalert2';
declare function initApp(): any;

@Component({
  selector: 'app-situacao-troca-novo',
  templateUrl: './situacao-troca-novo.component.html'
})

export class SituacaoTrocaNovoComponent implements OnInit {

  situacaoTrocaId: string;
  situacaoTrocaForm: FormGroup;
  isLoading = false;
  objSituacaoTroca: any;
  title: string;
  currentUser: any;

  constructor(public elementRef: ElementRef,
              public chRef: ChangeDetectorRef,
              public router: Router,
              public location: Location,
              public authService: AuthenticationService,
              public service: SituacaoTrocaService,
              public fb: FormBuilder,
              public activatedroute: ActivatedRoute) {

    activatedroute.params.subscribe(params => { this.situacaoTrocaId = params.situacaoTrocaId; });
    if (this.situacaoTrocaId === '' || this.situacaoTrocaId === undefined || this.situacaoTrocaId === null) {
      this.situacaoTrocaId = '';
      this.title = 'Cadastro de Situação de Troca';
    } else {
      this.title = 'Edição de Situação de Troca';
    }
    this.authService.currentUser.subscribe(x => this.currentUser = x);
    this.objSituacaoTroca = new Object();
    initApp();
  }

  ngOnInit() {
    this.situacaoTrocaForm = this.fb.group({
      id: new FormControl(''),
      ativo: new FormControl(''),
      descricao: new FormControl('', [Validators.required])
    });

    if (this.situacaoTrocaId !== '' && this.situacaoTrocaId !== undefined && this.situacaoTrocaId !== null) {
      this.service.ObterSituacaoTroca(this.situacaoTrocaId)
        .subscribe((resp) => {
            if (resp.success) {
              this.objSituacaoTroca = resp.obj;
              this.situacaoTrocaForm.controls.id.setValue(this.objSituacaoTroca.id);
              this.situacaoTrocaForm.controls.ativo.setValue(this.objSituacaoTroca.ativo);
              this.situacaoTrocaForm.controls.descricao.setValue(this.objSituacaoTroca.descricao);
            } else {
              this.objSituacaoTroca.id = '';
              this.objSituacaoTroca.ativo = 'true';
              this.objSituacaoTroca.descricao = '';
            }
        });
    } else {
        this.objSituacaoTroca.id = '';
        this.objSituacaoTroca.ativo = 'true';
        this.objSituacaoTroca.descricao = '';
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
        this.service.ConfirmarSituacaoTroca(
          this.situacaoTrocaForm.controls.id.value,
          this.situacaoTrocaForm.controls.descricao.value,
          this.situacaoTrocaForm.controls.ativo.value,
          this.currentUser.cs)
          .subscribe((recModel) => {
            if (recModel.success) {
              Swal.fire({
                title: 'Operação realizada!',
                html: 'Situação de Troca gravada no banco de dados',
                type: 'success',
                confirmButtonText: 'OK'
              })
              .then((res) => {
                this.router.navigate(['situacao-troca']);
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
