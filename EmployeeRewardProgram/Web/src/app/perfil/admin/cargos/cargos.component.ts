import { Component, OnInit, ChangeDetectorRef, ElementRef } from '@angular/core';
import { DTables } from 'src/app/_helpers/_dTables';

import { Router } from '@angular/router';
import { Location } from '@angular/common';
import Swal from 'sweetalert2';
import { CargosService } from './cargos.service';
import { AuthenticationService } from 'src/app/_services/authentication.service';
declare function initApp(): any;

@Component({
  selector: 'app-cargos',
  templateUrl: './cargos.component.html'
})

export class CargosComponent implements OnInit {

  dataSource: any[] = [];
  title: string;

  constructor(public elementRef: ElementRef,
              public chRef: ChangeDetectorRef,
              public router: Router,
              public location: Location,
              public authService: AuthenticationService,
              public dTb: DTables,
              public service: CargosService) {

      this.title = 'Cargos';
      initApp();
   }

  ngOnInit() {

    try {
      this.service.ObterCargos()
        .subscribe((resp) => {
          if (resp.success) {
            this.dataSource = resp.results;
            this.chRef.detectChanges();
            this.dTb.montaTable(this.dataSource);
          } else {
            Swal.fire({
              type: 'error',
              title: 'Oops...',
              text: 'Houve um erro ao tentar Obter os Cargos cadastrados!'
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

  elegivel(id: string = '', nome: string = '', ativo: string = 'true') {
    const bAtivo = ativo === 'true';
    Swal.fire({
      title: 'Confirmar Operação',
      html: 'Deseja marcar como ' + (bAtivo ? 'elegível' : 'inelegível' ) + ' o Cargo <b>' + nome + '</b>?',
      type: 'question',
      showCancelButton: true,
      confirmButtonText: 'Confirmar',
      cancelButtonText: 'Cancelar'
    })
    .then((result) => {
      if (result.value) {
        this.service.AtivarInativar(
          id,
          ativo)
          .subscribe((recModel) => {
            if (recModel.success) {
              Swal.fire({
                title: 'Operação realizada!',
                html: 'Cargo gravado no banco de dados',
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
