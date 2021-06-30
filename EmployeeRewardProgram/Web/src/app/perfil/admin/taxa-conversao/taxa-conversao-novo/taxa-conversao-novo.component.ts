import { Component, OnInit, ElementRef, ChangeDetectorRef } from '@angular/core';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { TaxaConversaoService } from '../taxa-conversao.service';
import * as $ from 'jquery';
import 'jquery-mask-plugin';
import 'datatables.net';
import 'datatables.net-bs4';
import Swal from 'sweetalert2';
import { AuthenticationService } from 'src/app/_services/authentication.service';
declare function initApp(): any;

@Component({
  selector: 'app-taxa-conversao-novo',
  templateUrl: './taxa-conversao-novo.component.html'
})

export class TaxaConversaoNovoComponent implements OnInit {

  taxaId: string;
  taxaForm: FormGroup;
  isLoading = false;
  objTaxa: any;
  title: string;
  currentUser: any;

  constructor(public elementRef: ElementRef,
              public chRef: ChangeDetectorRef,
              public router: Router,
              public location: Location,
              public authService: AuthenticationService,
              public service: TaxaConversaoService,
              public fb: FormBuilder,
              public activatedroute: ActivatedRoute) {

    activatedroute.params.subscribe(params => { this.taxaId = params.taxaId; });
    if (this.taxaId === '' || this.taxaId === undefined || this.taxaId === null) {
      this.title = 'Cadastro de Nova Taxa';
    } else {
      this.title = 'Edição de Taxa';
    }
    this.authService.currentUser.subscribe(x => this.currentUser = x);
    this.objTaxa = new Object();
    initApp();
  }

  async ngOnInit() {
    this.taxaForm = this.fb.group({
      id: new FormControl(''),
      ativo: new FormControl(''),
      valor_moeda: new FormControl(''),
      nome: new FormControl('', [Validators.required]),
      taxa: new FormControl('', [Validators.required])
    });

    if (this.taxaId !== '' && this.taxaId !== undefined && this.taxaId !== null) {
      await this.service.ObterTaxa(this.taxaId)
        .then((resp) => {
            this.chRef.detectChanges();
            if (resp.success) {
              this.objTaxa = resp.obj;
              this.taxaForm.controls.id.setValue(this.objTaxa.id);
              this.taxaForm.controls.ativo.setValue(this.objTaxa.ativo);
              this.taxaForm.controls.nome.setValue(this.objTaxa.nome);
              this.taxaForm.controls.taxa.setValue(this.objTaxa.fator);
              this.setValor(this.objTaxa.valor_moeda);
            } else {
              this.objTaxa.id = '';
              this.objTaxa.ativo = 'true';
              this.objTaxa.valor_moeda = '';
              this.objTaxa.nome = '';
              this.objTaxa.taxa = '';
            }
        });
    } else {
      this.objTaxa.id = '';
      this.objTaxa.ativo = 'true';
      this.objTaxa.valor_moeda = '';
      this.objTaxa.nome = '';
      this.objTaxa.taxa = '';
    }
    $('.valorMonetario').mask('#.##0,00', { reverse: true });
  }

  setValor(value: any) {
    if (value !== undefined && value !== null) {
      if (typeof(value) === 'string') {
        value = value.replace('.', '');
        value = value.replace(',', '.');
        value = parseFloat(value).toFixed(2);
      } else {
        value = value.toFixed(2);
      }
      this.taxaForm.controls.valor_moeda.setValue(value);
      $('.valorMonetario').mask('#.##0,00', { reverse: true });
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
        this.service.ConfirmarTaxa(
          this.taxaForm.controls.id.value,
          this.taxaForm.controls.ativo.value,
          this.taxaForm.controls.valor_moeda.value,
          this.taxaForm.controls.taxa.value,
          this.taxaForm.controls.nome.value,
          this.currentUser.cs)
          .subscribe((recModel) => {
            if (recModel.success) {
              Swal.fire({
                title: 'Operação realizada!',
                html: 'Taxa de Conversão gravada no banco de dados',
                type: 'success',
                confirmButtonText: 'OK'
              })
              .then((res) => {
                this.router.navigate(['taxa-conversao']);
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
