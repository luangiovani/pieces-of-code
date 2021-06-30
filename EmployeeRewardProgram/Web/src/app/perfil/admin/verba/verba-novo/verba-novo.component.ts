import { Component, OnInit, ElementRef, ChangeDetectorRef } from '@angular/core';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { VerbaService } from '../verba.service';
import { ColaboradorService } from '../../colaboradores/colaborador.service';
import { TaxaConversaoService } from '../../taxa-conversao/taxa-conversao.service';
import * as $ from 'jquery';
import 'datatables.net';
import 'datatables.net-bs4';
import 'jquery-mask-plugin';
import Swal from 'sweetalert2';
declare function initApp(): any;

@Component({
  selector: 'app-verba-novo',
  templateUrl: './verba-novo.component.html'
})

export class VerbaNovoComponent implements OnInit {

  verbaId: string;
  verbaForm: FormGroup;
  objVerba: any;
  listColaboradores: any[];
  taxaAtual: any;
  title: string;
  currentUser: any;

  constructor(public elementRef: ElementRef,
              public chRef: ChangeDetectorRef,
              public router: Router,
              public location: Location,
              public authService: AuthenticationService,
              public service: VerbaService,
              public colaboradorService: ColaboradorService,
              public taxaService: TaxaConversaoService,
              public fb: FormBuilder,
              public activatedroute: ActivatedRoute) {

    activatedroute.params.subscribe(params => { this.verbaId = params.verbaId; });
    if (this.verbaId === '' || this.verbaId === undefined || this.verbaId === null) {
      this.title = 'Atribuir Verba';
    } else {
      this.title = 'Editar Verba';
    }
    this.authService.currentUser.subscribe(x => this.currentUser = x);
    this.objVerba = new Object();
    initApp();
  }

  async ngOnInit() {

    this.verbaForm = this.fb.group({
      id: new FormControl(''),
      ativo: new FormControl(''),
      csColaborador: new FormControl('', [Validators.required]),
      valorPontos: new FormControl('', [Validators.required]),
      valorMoeda: new FormControl(''),
      observacoes: new FormControl('')
    });

    this.verbaForm.controls.valorMoeda.disable();

    try {
      await this.taxaService.ObterAtual()
        .then((resp) => {
          if (resp.success) {
            this.taxaAtual = resp.obj;
          } else {
            Swal.fire({
              type: 'error',
              title: 'Oops...',
              text: 'Houve um erro ao tentar Obter a Taxa de Conversão cadastrada!'
            });
          }
        });

      await this.colaboradorService.ObterColaboradoresGestores()
        .then((resp) => {
          if (resp.success) {
              this.listColaboradores = resp.results;
          } else {
            Swal.fire({
              type: 'error',
              title: 'Oops...',
              text: 'Houve um erro ao tentar Obter os colaboradores cadastrados!'
            });
          }
        });

      if (this.verbaId !== '' && this.verbaId !== undefined && this.verbaId !== null) {
        this.chRef.detectChanges();
        this.verbaForm.controls.csColaborador.disable();
        await this.service.ObterVerba(this.verbaId)
          .then((resp) => {
            if (resp.success) {
              this.objVerba = resp.obj;
              this.verbaForm.controls.id.setValue(this.objVerba.id);
              this.verbaForm.controls.ativo.setValue(this.objVerba.ativo);
              this.verbaForm.controls.csColaborador.setValue(this.objVerba.cs_colaborador);
              this.verbaForm.controls.valorPontos.setValue(this.objVerba.valor_pontos);
              this.verbaForm.controls.valorMoeda.setValue(this.objVerba.valor_moeda);
              this.verbaForm.controls.observacoes.setValue(this.objVerba.observacao);
              this.setPontos(this.objVerba.valor_moeda);
              this.setValor(this.objVerba.valor_pontos);
            } else {
              this.verbaForm.controls.csColaborador.enable();
              this.objVerba.id = '';
              this.objVerba.ativo = 'true';
              this.objVerba.cs_colaborador = '';
              this.objVerba.valor_pontos = '';
              this.objVerba.valor_moeda = '';
              this.objVerba.observacao = '';
              Swal.fire({
                type: 'error',
                title: 'Oops...',
                text: 'Houve um erro ao tentar Obter a Verba cadastrada!'
              });
            }
          });
      } else {
        this.verbaForm.controls.csColaborador.enable();
        this.objVerba.id = '';
        this.objVerba.ativo = 'true';
        this.objVerba.cs_colaborador = '';
        this.objVerba.valor_pontos = '';
        this.objVerba.valor_moeda = '';
      }
    } catch (err) {
        Swal.fire({
          type: 'warning',
          title: 'Oops...',
          text: 'Ocorreu um erro interno.: ' + err.message
        });
    }

    this.chRef.detectChanges();
    $('.valorMonetario').mask('#.##0,00', { reverse: true });
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

        this.objVerba.valor_pontos = this.verbaForm.controls.valorPontos.value;
        if (typeof(this.objVerba.valor_pontos) === 'string') {
          this.objVerba.valor_pontos = this.objVerba.valor_pontos.replace('.', '');
          this.objVerba.valor_pontos = this.objVerba.valor_pontos.replace(',', '.');
        }

        this.objVerba.valor_moeda = this.verbaForm.controls.valorMoeda.value;
        if (typeof(this.objVerba.valor_moeda) === 'string') {
          this.objVerba.valor_moeda = this.objVerba.valor_moeda.replace('.', '');
          this.objVerba.valor_moeda = this.objVerba.valor_moeda.replace(',', '.');
        }

        this.service.ConfirmarVerba(this.verbaForm.controls.id.value,
                                    this.verbaForm.controls.ativo.value,
                                    this.verbaForm.controls.csColaborador.value,
                                    this.verbaForm.controls.valorPontos.value,
                                    this.verbaForm.controls.valorMoeda.value,
                                    this.verbaForm.controls.observacoes.value,
                                    this.currentUser.cs)
          .subscribe((recModel) => {
            if (recModel.success) {
              Swal.fire({
                title: 'Operação realizada!',
                html: 'Verba gravada no banco de dados',
                type: 'success',
                confirmButtonText: 'OK'
              })
              .then((res) => {
                this.router.navigate(['verba']);
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

  setPontos(value: any) {
    if (value !== undefined && value !== null && value !== '') {
      if (typeof(value) === 'string') {
        value = value.replace('.', '');
        value = value.replace(',', '.');
        value = parseFloat(value).toFixed(2);
      }
      this.chRef.detectChanges();
      if (this.taxaAtual === undefined || this.taxaAtual === null) {
        value = value / 1000;
      } else {
        value = value / this.taxaAtual.taxa;
      }
      this.verbaForm.controls.valorPontos.setValue(value.toFixed(0));
    }
  }

  setValor(value: any) {
    if (value !== undefined && value !== null && value !== '') {
      if (typeof(value) === 'string') {
        value = value.replace('.', '');
        value = value.replace(',', '.');
        value = parseFloat(value).toFixed(2);
      }
      this.chRef.detectChanges();
      if (this.taxaAtual === undefined || this.taxaAtual === null) {
        value = (value * 1000).toLocaleString('pt-BR', { minimumFractionDigits: 2 });
      } else {
        value = (value * this.taxaAtual.taxa).toLocaleString('pt-BR', { minimumFractionDigits: 2 });
      }
      this.verbaForm.controls.valorMoeda.setValue(value);
      $('.valorMonetario').mask('#.##0,00', { reverse: true });
    }
  }
}
