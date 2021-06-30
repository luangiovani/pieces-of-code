import { Component, OnInit, ElementRef, ChangeDetectorRef } from '@angular/core';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import { ConfigurarVerbasService } from '../configurar-verbas/configurar-verbas.service';
import * as $ from 'jquery';
import 'datatables.net';
import 'datatables.net-bs4';
import 'jquery-mask-plugin';
import Swal from 'sweetalert2';
import { AuthenticationService } from 'src/app/_services/authentication.service';
declare function initApp(): any;

@Component({
  selector: 'app-configurar-verbas',
  templateUrl: './configurar-verbas.component.html'
})

export class ConfigurarVerbasComponent implements OnInit {

  cfgVerbasForm: FormGroup;
  isLoading = false;
  objcfgVerbas: any;
  title: string;

  constructor(public elementRef: ElementRef,
              public chRef: ChangeDetectorRef,
              public router: Router,
              public location: Location,
              public authService: AuthenticationService,
              public service: ConfigurarVerbasService,
              public fb: FormBuilder) {
    this.title = 'Configuração de Distribuição de Verbas';
    this.objcfgVerbas = new Object();
    initApp();
  }

  async ngOnInit() {
    this.cfgVerbasForm = this.fb.group({
      id: new FormControl(''),
      ativo: new FormControl(''),
      minPontos: new FormControl('', [Validators.required]),
      pontosPorColaborador: new FormControl('', [Validators.required]),
      pontosPorArea: new FormControl('', [Validators.required]),
      dataDisponibilidade: new FormControl(''),
      dataBloqueio: new FormControl(''),
      labelLoja: new FormControl('')
    });

    try {
      await this.service.ObterConfiguracaoVerba()
      .then((resp) => {
        if (resp.success) {
          if (resp.obj) {
            this.objcfgVerbas = resp.obj;
            this.cfgVerbasForm.controls.id.setValue(this.objcfgVerbas.id);
            this.cfgVerbasForm.controls.ativo.setValue(this.objcfgVerbas.ativo);
            this.cfgVerbasForm.controls.minPontos.setValue(this.objcfgVerbas.pontos_minimos);
            this.cfgVerbasForm.controls.pontosPorColaborador.setValue(this.objcfgVerbas.pontos_por_colaborador);
            this.cfgVerbasForm.controls.pontosPorArea.setValue(this.objcfgVerbas.pontos_por_area);
            this.cfgVerbasForm.controls.dataDisponibilidade.setValue(this.objcfgVerbas.dt_disponivel);
            this.cfgVerbasForm.controls.dataBloqueio.setValue(this.objcfgVerbas.dt_bloquear);
          } else {
            this.objcfgVerbas.id = '';
            this.objcfgVerbas.ativo = 'true';
            this.objcfgVerbas.pontos_minimos = '';
            this.objcfgVerbas.pontos_por_colaborador = '';
            this.objcfgVerbas.pontos_por_area = '';
            this.objcfgVerbas.dt_disponivel = '';
            this.objcfgVerbas.dt_bloquear = '';
          }
        } else {
          this.objcfgVerbas.id = '';
          this.objcfgVerbas.ativo = 'true';
          this.objcfgVerbas.pontos_minimos = '';
          this.objcfgVerbas.pontos_por_colaborador = '';
          this.objcfgVerbas.pontos_por_area = '';
          this.objcfgVerbas.dt_disponivel = '';
          this.objcfgVerbas.dt_bloquear = '';

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

    this.chRef.detectChanges();
    $('.datepicker').datepicker({
      format: 'dd/mm/yyyy',
      autoclose: true,
      orientation: 'bottom',
      locale: 'pt-BR'
    });
  }

  setDateDisponibilidade(value: string) {
    this.chRef.detectChanges();
    this.cfgVerbasForm.controls.dataDisponibilidade.setValue(value);
  }

  setDateBloqueio(value: string) {
    this.chRef.detectChanges();
    this.cfgVerbasForm.controls.dataBloqueio.setValue(value);
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
        this.service.ConfirmarConfiguracaoVerba(
          this.cfgVerbasForm.controls.id.value,
          this.cfgVerbasForm.controls.ativo.value,
          this.cfgVerbasForm.controls.minPontos.value,
          this.cfgVerbasForm.controls.pontosPorColaborador.value,
          this.cfgVerbasForm.controls.pontosPorArea.value,
          this.cfgVerbasForm.controls.dataDisponibilidade.value,
          this.cfgVerbasForm.controls.dataBloqueio.value)
          .subscribe((recModel) => {
            if (recModel.success) {
              Swal.fire({
                title: 'Operação realizada!',
                html: 'Configuração de Distribuição de Verba gravada no banco de dados',
                type: 'success',
                confirmButtonText: 'OK'
              })
              .then((res) => {
                this.router.navigate(['configurar-verbas']);
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
