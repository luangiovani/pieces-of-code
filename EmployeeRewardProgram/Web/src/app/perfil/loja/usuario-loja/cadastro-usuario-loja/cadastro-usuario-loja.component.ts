import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { DTables } from 'src/app/_helpers/_dTables';
import { AuthenticationService } from 'src/app/_services';
import Swal from 'sweetalert2';
import { LojasService } from 'src/app/perfil/admin/lojas/lojas.service';
declare function initApp(): any;

@Component({
  selector: 'app-cadastro-usuario-loja',
  templateUrl: './cadastro-usuario-loja.component.html',
  styleUrls: ['./cadastro-usuario-loja.component.css']
})
export class CadastroUsuarioLojaComponent implements OnInit {

  title: string;
  vincularForm: FormGroup;
  objColaboradorLoja: any;
  lojas: any[] = [];
  colaboradores: any[] = [];
  colaboradorCS = '';
  lojaId = '';
  constructor(public chRef: ChangeDetectorRef,
              public router: Router,
              public location: Location,
              public fb: FormBuilder,
              public activatedroute: ActivatedRoute,
              public dTb: DTables,
              public authService: AuthenticationService,
              public service: LojasService) {
    activatedroute.params.subscribe(params => { this.colaboradorCS = params.id; });
    if (this.colaboradorCS === undefined || this.colaboradorCS === null) {
      this.colaboradorCS = '';
    }

    activatedroute.params.subscribe(params => { this.lojaId = params.loja_id; });
    if (this.lojaId === undefined || this.lojaId === null) {
      this.lojaId = '';
    }

    initApp();
    this.title = 'Vincular Colaborador Loja';

    this.vincularForm = this.fb.group({
      vinculo_id: new FormControl(''),
      cs: new FormControl('', [Validators.required]),
      loja_id: new FormControl('', [Validators.required])
    });

    this.objColaboradorLoja = new Object();
    this.objColaboradorLoja.id = '';
    this.objColaboradorLoja.cs = this.colaboradorCS;
    this.objColaboradorLoja.loja_id = this.lojaId;
  }

  async ngOnInit() {
    this.chRef.detectChanges();
    await this.service.ObterLojasAsync()
      .then((resp) => {
        if (resp.success) {
          this.lojas = resp.results;
        } else {
          Swal.fire({
            type: 'error',
            title: 'Oops...',
            text: 'Ocorreu um erro interno.: ' + resp.message
          });
          return;
        }
      });

    await this.service.ObterColaboradoresLojaAsync()
      .then((resp) => {
        if (resp.success) {
          this.colaboradores = resp.results;
        } else {
          Swal.fire({
            type: 'error',
            title: 'Oops...',
            text: 'Ocorreu um erro interno.: ' + resp.message
          });
          return;
        }
      });

    if (this.colaboradorCS !== '') {
      this.vincularForm.controls.cs.setValue(this.colaboradorCS);
      this.vincularForm.controls.cs.disable();
    }

    if (this.lojaId !== '') {
      this.vincularForm.controls.loja_id.setValue(this.lojaId.toUpperCase());
    }

    if (this.colaboradorCS !== '' && this.lojaId !== '') {
      this.chRef.detectChanges();
      try {
        await this.service.ObterColaboradorAsync(this.colaboradorCS, this.lojaId)
          .then((resp) => {
            if (resp.success && resp.obj !== null && resp.obj !== undefined) {
              this.vincularForm.controls.vinculo_id.setValue(resp.obj.id);
              this.objColaboradorLoja.id = resp.obj.id;
              this.vincularForm.controls.cs.disable();
            }
          });
        } catch (err) {
            Swal.fire({
              type: 'warning',
              title: 'Oops...',
              text: 'Ocorreu um erro interno.: ' + err.message
            });
        }
      }
  }

  vincular() {
    this.chRef.detectChanges();
    try {
      if (this.vincularForm.controls.cs.value !== '' && this.vincularForm.controls.loja_id.value !== '') {
        this.service.ConfirmarColaboradorLoja(this.vincularForm.controls.vinculo_id.value,
                                              this.vincularForm.controls.cs.value,
                                              this.vincularForm.controls.loja_id.value)
          .subscribe((resp) => {
            if (resp.success) {
              Swal.fire({
                type: 'success',
                title: 'Operação Realizada',
                text: 'O vinculo entre Colaborador e Loja foi realizado com sucesso!'
              }).then((r) => {
                this.router.navigate(['usuario-loja']);
              });
            } else {
              Swal.fire({
                type: 'error',
                title: 'Oops...',
                text: resp.message
              });
            }
          });
      } else {
        Swal.fire({
          type: 'error',
          title: 'Oops...',
          text: 'Os campos obrigatórios não foram preenchidos, consulte um colaborador para prosseguir.'
        });
      }
    } catch (error) {
      Swal.fire({
        type: 'error',
        title: 'Oops...',
        text: 'Ocorreu um erro na solicitação.: ' + error.message
      });
    }
  }

  goback() {
    this.location.back();
  }

}
