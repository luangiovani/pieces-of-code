import { Component, OnInit, ElementRef, ChangeDetectorRef } from '@angular/core';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { OpcoesEntregaService } from '../opcoes-entrega.service';
import 'datatables.net';
import 'datatables.net-bs4';
import Swal from 'sweetalert2';
import { AuthenticationService } from 'src/app/_services/authentication.service';
declare function initApp(): any;
@Component({
  selector: 'app-opcoes-entrega-novo',
  templateUrl: './opcoes-entrega-novo.component.html'
})

export class OpcoesEntregaNovoComponent implements OnInit {

  opcaoId: string;
  opcaoForm: FormGroup;
  isLoading = false;
  objOpcao: any;
  title: string;
  currentUser: any;

  constructor(public elementRef: ElementRef,
              public chRef: ChangeDetectorRef,
              public router: Router,
              public location: Location,
              public authService: AuthenticationService,
              public service: OpcoesEntregaService,
              public fb: FormBuilder,
              public activatedroute: ActivatedRoute) {

    activatedroute.params.subscribe(params => { this.opcaoId = params.opcaoId; });
    if (this.opcaoId === '' || this.opcaoId === undefined || this.opcaoId === null) {
      this.title = 'Cadastro de Nova Opção de Entrega';
    } else {
      this.title = 'Edição de Opção de Entrega';
    }
    this.authService.currentUser.subscribe(x => this.currentUser = x);
    this.objOpcao = new Object();
    initApp();
  }

  async ngOnInit() {
    this.opcaoForm = this.fb.group({
      id: new FormControl(''),
      ativo: new FormControl(''),
      label: new FormControl('', [Validators.required]),
      labelColaborador: new FormControl('', [Validators.required]),
      labelLoja: new FormControl('', [Validators.required])
    });

    this.chRef.detectChanges();
    if (this.opcaoId !== '' && this.opcaoId !== undefined && this.opcaoId !== null) {
      await this.service.ObterOpcaoEntrega(this.opcaoId)
        .then((resp) => {
            if (resp.success) {
              this.objOpcao = resp.obj;
              this.opcaoForm.controls.id.setValue(this.objOpcao.id);
              this.opcaoForm.controls.ativo.setValue(this.objOpcao.ativo);
              this.opcaoForm.controls.label.setValue(this.objOpcao.label);
              this.opcaoForm.controls.labelColaborador.setValue(this.objOpcao.label_colaborador);
              this.opcaoForm.controls.labelLoja.setValue(this.objOpcao.label_loja);
            } else {
              this.objOpcao.id = '';
              this.objOpcao.ativo = 'true';
              this.objOpcao.label = '';
              this.objOpcao.label_colaborador = '';
              this.objOpcao.label_loja = '';
            }
        });
    } else {
      this.objOpcao.id = '';
      this.objOpcao.ativo = 'true';
      this.objOpcao.label = '';
      this.objOpcao.label_colaborador = '';
      this.objOpcao.label_loja = '';
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
        this.service.ConfirmarOpcaoEntrega(
          this.opcaoForm.controls.id.value,
          this.opcaoForm.controls.ativo.value,
          this.opcaoForm.controls.label.value,
          this.opcaoForm.controls.labelColaborador.value,
          this.opcaoForm.controls.labelLoja.value,
          this.currentUser.cs)
          .subscribe((recModel) => {
            if (recModel.success) {
              Swal.fire({
                title: 'Operação realizada!',
                html: 'Opção de Entrega gravada no banco de dados',
                type: 'success',
                confirmButtonText: 'OK'
              })
              .then((res) => {
                this.router.navigate(['opcoes-entrega']);
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
