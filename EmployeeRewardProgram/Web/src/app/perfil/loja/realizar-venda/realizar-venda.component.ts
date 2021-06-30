import { Component, OnInit, ChangeDetectorRef, ElementRef } from '@angular/core';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { RealizarVendaService } from './realizar-venda.service';
import { DTables } from 'src/app/_helpers/_dTables';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { AuthenticationService } from 'src/app/_services';
declare function initApp(): any;

@Component({
  selector: 'app-realizar-venda',
  templateUrl: './realizar-venda.component.html'
})
export class RealizarVendaComponent implements OnInit {

  title: string;
  currentUser: any;
  objSaldoPontos: any;
  compraForm: FormGroup;
  objColaborador: any;

  constructor(public chRef: ChangeDetectorRef,
              public router: Router,
              public fb: FormBuilder,
              public dTb: DTables,
              public authService: AuthenticationService,
              public service: RealizarVendaService) {
    initApp();
    this.title = 'Trocar Pontos por Produto';
    this.authService.currentUser.subscribe(x => this.currentUser = x);
    this.compraForm = this.fb.group({
      colaborador_id: new FormControl('', [Validators.required]),
      colaborador_cs: new FormControl('', [Validators.required]),
      colaborador_nome: new FormControl('', [Validators.required]),
      colaborador_pontos: new FormControl('')
    });

    this.compraForm.controls.colaborador_nome.disable();
    this.compraForm.controls.colaborador_pontos.disable();

    this.objColaborador = new Object();
    this.objColaborador.id = '';
    this.objColaborador.nome = '';
    this.objColaborador.quantidade_pontos = 0;
    this.objColaborador.cs = '';
  }

  ngOnInit() {
    this.chRef.detectChanges();
  }

  obterColaborador() {
    this.chRef.detectChanges();
    try {
      this.service.ObterColaborador(this.compraForm.controls.colaborador_cs.value)
        .subscribe((resp) => {
          if (resp.success && resp.obj !== null && resp.obj !== undefined) {
            this.objColaborador = resp.obj;
            this.compraForm.controls.colaborador_id.setValue(resp.obj.id);
          } else {
            Swal.fire({
              type: 'warning',
              title: 'Oops...',
              text: 'Ocorreu um erro interno.: ' + resp.message
            });
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

  prosseguir() {
    this.chRef.detectChanges();
    if (this.compraForm.controls.colaborador_id.value !== '') {
      if (this.objColaborador.quantidade_pontos === 0) {
        Swal.fire({
          type: 'error',
          title: 'Oops...',
          text: 'O Colaborador não possui pontos para troca.'
        });
      } else {
        this.router.navigate(['listar-produtos/' + this.objColaborador.cs]);
      }
    } else {
      Swal.fire({
        type: 'error',
        title: 'Oops...',
        text: 'Os campos obrigatórios não foram preenchidos, consulte um colaborador para prosseguir.'
      });
    }
   }
}
