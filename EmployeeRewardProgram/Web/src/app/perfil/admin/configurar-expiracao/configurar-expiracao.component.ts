import { Component, OnInit, ElementRef, ChangeDetectorRef } from '@angular/core';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import { ConfigurarExpiracaoService } from '../configurar-expiracao/configurar-expiracao.service';
import Swal from 'sweetalert2';
import { AuthenticationService } from 'src/app/_services/authentication.service';
declare function initApp(): any;

@Component({
  selector: 'app-configurar-expiracao',
  templateUrl: './configurar-expiracao.component.html'
})

export class ConfigurarExpiracaoComponent implements OnInit {

  cfgExpiracaoForm: FormGroup;
  objcfgExpiracao: any;
  title: string;

  constructor(public elementRef: ElementRef,
              public chRef: ChangeDetectorRef,
              public router: Router,
              public location: Location,
              public authService: AuthenticationService,
              public service: ConfigurarExpiracaoService,
              public fb: FormBuilder) {

    this.title = 'Configuração de Expiração de Pontos do Colaborador';
    this.objcfgExpiracao = new Object();
    initApp();
  }

  async ngOnInit() {
    this.cfgExpiracaoForm = this.fb.group({
      id: new FormControl(''),
      ativo: new FormControl(''),
      qtdeExpiracao: new FormControl('', [Validators.required]),
      tipoExpiracao: new FormControl('', [Validators.required]),
      qtdeExpiracaoDesligamento: new FormControl(''),
      tipoExpiracaoDesligamento: new FormControl('')
    });

    try {
      this.chRef.detectChanges();
      await this.service.ObterConfiguracaoExpiracao()
      .then((resp) => {
        if (resp.success) {
          if (resp.obj) {
            this.objcfgExpiracao = resp.obj;
            this.cfgExpiracaoForm.controls.id.setValue(this.objcfgExpiracao.id);
            this.cfgExpiracaoForm.controls.ativo.setValue(this.objcfgExpiracao.ativo);
            this.cfgExpiracaoForm.controls.qtdeExpiracao.setValue(this.objcfgExpiracao.qtde_expiracao);
            this.cfgExpiracaoForm.controls.tipoExpiracao.setValue(this.objcfgExpiracao.tipo_expiracao);
            this.cfgExpiracaoForm.controls.qtdeExpiracaoDesligamento.setValue(this.objcfgExpiracao.qtde_expiracao_desligamento);
            this.cfgExpiracaoForm.controls.tipoExpiracaoDesligamento.setValue(this.objcfgExpiracao.tipo_expiracao_desligamento);
          } else {
            this.objcfgExpiracao.id = '';
            this.objcfgExpiracao.ativo = 'true';
            this.objcfgExpiracao.qtde_expiracao = '';
            this.objcfgExpiracao.tipo_expiracao = '';
            this.objcfgExpiracao.qtde_expiracao_desligamento = '';
            this.objcfgExpiracao.tipo_expiracao_desligamento = '';
          }
        } else {
          this.objcfgExpiracao.id = '';
          this.objcfgExpiracao.ativo = 'true';
          this.objcfgExpiracao.qtde_expiracao = '';
          this.objcfgExpiracao.tipo_expiracao = '';
          this.objcfgExpiracao.qtde_expiracao_desligamento = '';
          this.objcfgExpiracao.tipo_expiracao_desligamento = '';

          Swal.fire({
            title: 'Erro ao tentar obter Configuração!',
            html: resp.message,
            type: 'error',
            confirmButtonText: 'OK'
          });
        }
      });
    } catch (err) {
      Swal.fire({
        title: 'Operação não realizada!',
        html: err.message,
        type: 'error',
        confirmButtonText: 'OK'
      });
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
        this.service.ConfirmarExpiracaoPontos(
          this.cfgExpiracaoForm.controls.id.value,
          this.cfgExpiracaoForm.controls.ativo.value,
          this.cfgExpiracaoForm.controls.qtdeExpiracao.value,
          this.cfgExpiracaoForm.controls.tipoExpiracao.value,
          this.cfgExpiracaoForm.controls.qtdeExpiracaoDesligamento.value,
          this.cfgExpiracaoForm.controls.tipoExpiracaoDesligamento.value)
          .subscribe((recModel) => {
            if (recModel.success) {
              Swal.fire({
                title: 'Operação realizada!',
                html: 'Configuração de Expiração de Pontos de Colaboradores gravado no banco de dados',
                type: 'success',
                confirmButtonText: 'OK'
              })
              .then((res) => {
                this.router.navigate(['configurar-expiracao']);
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
