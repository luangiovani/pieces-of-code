import { Component, OnInit, ElementRef, ChangeDetectorRef } from '@angular/core';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { MeioCompraService } from '../meio-compra.service';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import Swal from 'sweetalert2';
declare function initApp(): any;

@Component({
  selector: 'app-meio-compra-novo',
  templateUrl: './meio-compra-novo.component.html'
})
export class MeioCompraNovoComponent implements OnInit {

  meioId: string;
  meioForm: FormGroup;
  isLoading = false;
  objMeio: any;
  listMeios: any[] = [];
  title: string;
  currentUser: any;
  constructor(public elementRef: ElementRef,
              public chRef: ChangeDetectorRef,
              public router: Router,
              public location: Location,
              public authService: AuthenticationService,
              public service: MeioCompraService,
              public fb: FormBuilder,
              public activatedroute: ActivatedRoute) {

    activatedroute.params.subscribe(params => { this.meioId = params.meioId; });
    if (this.meioId === '' || this.meioId === undefined || this.meioId === null) {
      this.meioId = '';
      this.title = 'Cadastro de Meio de Compra';
    } else {
      this.title = 'Edição de Meio de Compra';
    }
    this.authService.currentUser.subscribe(x => this.currentUser = x);
    this.objMeio = new Object();
    initApp();
  }

  ngOnInit() {
    this.meioForm = this.fb.group({
      id: new FormControl(''),
      ativo: new FormControl(''),
      nome: new FormControl('', [Validators.required])
    });

    if (this.meioId !== '' && this.meioId !== undefined && this.meioId !== null) {
      this.service.ObterMeioDeCompra(this.meioId)
        .subscribe((resp) => {
            if (resp.success) {
              this.objMeio = resp.obj;
              this.objMeio.controls.id.setValue(this.objMeio.id);
              this.objMeio.controls.nome.setValue(this.objMeio.nome);
              this.objMeio.controls.ativo.setValue(this.objMeio.ativo);
            } else {
              this.objMeio.ativo = 'true';
              this.objMeio.id = '';
              this.objMeio.nome = '';
            }
        });
    } else {
      this.objMeio.ativo = 'true';
      this.objMeio.id = '';
      this.objMeio.nome = '';
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
        this.service.ConfirmarMeioDeCompra(
          this.meioForm.controls.id.value,
          this.meioForm.controls.nome.value,
          this.meioForm.controls.ativo.value,
          this.currentUser.cs)
          .subscribe((recModel) => {
            if (recModel.success) {
              Swal.fire({
                title: 'Operação realizada!',
                html: 'Meio de Compra gravado no banco de dados',
                type: 'success',
                confirmButtonText: 'OK'
              })
              .then((res) => {
                this.router.navigate(['meio-compra']);
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
