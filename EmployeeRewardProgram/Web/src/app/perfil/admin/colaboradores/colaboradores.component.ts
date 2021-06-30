import { Component, OnInit, ChangeDetectorRef, ElementRef } from '@angular/core';
import { DTables } from 'src/app/_helpers/_dTables';

import { Router } from '@angular/router';
import { Location } from '@angular/common';
import Swal from 'sweetalert2';
import { ColaboradorService } from './colaborador.service';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { PerfilAcessoService } from '../../admin-tec/perfil-acesso/perfil-acesso.service';
declare function initApp(): any;
@Component({
  selector: 'app-colaboradores',
  templateUrl: './colaboradores.component.html'
})

export class ColaboradoresComponent implements OnInit {

  iptOpt = new Map<string, string>();

  dataSource: any[] = [];
  title: string;

  constructor(public elementRef: ElementRef,
              public chRef: ChangeDetectorRef,
              public router: Router,
              public location: Location,
              public authService: AuthenticationService,
              public dTb: DTables,
              public service: ColaboradorService,
              public perfilService: PerfilAcessoService) {

      this.title = 'Colaboradores';
      initApp();
   }

  ngOnInit() {
    try {
      this.perfilService.ObterPerfis()
        .subscribe((resp) => {
          if (resp.success) {
            resp.results.forEach(p => {
              this.iptOpt.set(p.id.toUpperCase(), p.nome);
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

    try {
      this.service.ObterColaboradores()
        .subscribe((resp) => {
          if (resp.success) {
            this.dataSource = resp.results;
            this.chRef.detectChanges();
            this.dTb.montaTable(this.dataSource);
          } else {
            Swal.fire({
              type: 'error',
              title: 'Oops...',
              text: 'Houve um erro ao tentar Obter os Colaboradores cadastrados!'
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

  mudarPerfil(id: string = '', nome: string = '', perfilId: string = '', perfil: string = '') {
    this.chRef.detectChanges();
    Swal.fire({
      title: 'Alterar Perfil do Colaborador ' + nome + '?',
      input: 'select',
      inputOptions: this.iptOpt,
      inputValue: perfilId.toUpperCase(),
      type: 'question',
      showCancelButton: true,
      confirmButtonText: 'Confirmar',
      cancelButtonText: 'Cancelar'
    })
    .then((result) => {
      if (result.value) {
        this.service.MudarPerfil(
          id,
          result.value,
          this.iptOpt.get(result.value))
          .subscribe((recModel) => {
            if (recModel.success) {
              Swal.fire({
                title: 'Operação realizada!',
                html: 'Aplicação gravada no banco de dados',
                type: 'success',
                confirmButtonText: 'OK'
              })
              .then((res) => {
                window.location.reload();
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
}
