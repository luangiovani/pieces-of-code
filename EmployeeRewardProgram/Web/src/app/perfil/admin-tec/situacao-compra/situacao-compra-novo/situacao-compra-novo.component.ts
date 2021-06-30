import { Component, OnInit, ElementRef, ChangeDetectorRef } from '@angular/core';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { SituacaoCompraService } from '../situacao-compra.service';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import Swal from 'sweetalert2';
declare function initApp(): any;

@Component({
  selector: 'app-situacao-compra-novo',
  templateUrl: './situacao-compra-novo.component.html'
})

export class SituacaoCompraNovoComponent implements OnInit {

  situacaoCompraId: string;
  situacaoCompraForm: FormGroup;
  isLoading = false;
  objSituacaoCompra: any;
  title: string;
  currentUser: any;

  constructor(public elementRef: ElementRef,
              public chRef: ChangeDetectorRef,
              public router: Router,
              public location: Location,
              public authService: AuthenticationService,
              public service: SituacaoCompraService,
              public fb: FormBuilder,
              public activatedroute: ActivatedRoute) {
    activatedroute.params.subscribe(params => { this.situacaoCompraId = params.situacaoCompraId; });
    if (this.situacaoCompraId === '' || this.situacaoCompraId === undefined || this.situacaoCompraId === null) {
      this.situacaoCompraId = '';
      this.title = 'Cadastro de Situação de Compra';
    } else {
      this.title = 'Edição de Situação de Compra';
    }
    this.authService.currentUser.subscribe(x => this.currentUser = x);
    this.objSituacaoCompra = new Object();
    initApp();
  }

  ngOnInit() {
    this.situacaoCompraForm = this.fb.group({
      id: new FormControl(''),
      ativo: new FormControl(''),
      descricao: new FormControl('', [Validators.required])
    });

    if (this.situacaoCompraId !== '' && this.situacaoCompraId !== undefined && this.situacaoCompraId !== null) {
      this.service.ObterSituacaoCompra(this.situacaoCompraId)
        .subscribe((resp) => {
            if (resp.success) {
              this.objSituacaoCompra = resp.obj;
              this.situacaoCompraForm.controls.id.setValue(this.objSituacaoCompra.id);
              this.situacaoCompraForm.controls.ativo.setValue(this.objSituacaoCompra.ativo);
              this.situacaoCompraForm.controls.descricao.setValue(this.objSituacaoCompra.descricao);
            } else {
              this.objSituacaoCompra.id = '';
              this.objSituacaoCompra.ativo = 'true';
              this.objSituacaoCompra.descricao = '';
            }
        });
    } else {
        this.objSituacaoCompra.id = '';
        this.objSituacaoCompra.ativo = 'true';
        this.objSituacaoCompra.descricao = '';
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
        this.service.ConfirmarSituacaoCompra(
          this.situacaoCompraForm.controls.id.value,
          this.situacaoCompraForm.controls.descricao.value,
          this.situacaoCompraForm.controls.ativo.value,
          this.currentUser.cs)
          .subscribe((recModel) => {
            if (recModel.success) {
              Swal.fire({
                title: 'Operação realizada!',
                html: 'Situação de Compra gravada no banco de dados',
                type: 'success',
                confirmButtonText: 'OK'
              })
              .then((res) => {
                this.router.navigate(['situacao-compra']);
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
