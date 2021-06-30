import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';
import { DTables } from 'src/app/_helpers/_dTables';
import { MinhasTrocasService } from './minhas-trocas.service';
import Swal from 'sweetalert2';
declare function initApp(): any;

@Component({
  selector: 'app-minhas-trocas',
  templateUrl: './minhas-trocas.component.html'
})

export class MinhasTrocasComponent implements OnInit {

  enums: any;
  dataSource: any[] = [];
  title: string;

  constructor(public chRef: ChangeDetectorRef,
              public router: Router,
              public dTb: DTables,
              public service: MinhasTrocasService) {
    this.title = 'Minhas Trocas Realizadas';
    initApp();
    this.enums = environment.enumns;
  }

  ngOnInit() {
    try {
      this.service.ObterRelatorio()
      .subscribe((resp) => {
        if (resp.success) {
          this.dataSource = resp.results;
          this.chRef.detectChanges();
          this.dTb.montaTable(this.dataSource);
          this.dTb.ajusta_filter();
          this.dTb.ajusta_label();
        } else {
          Swal.fire('Ocorreu um erro ao tentar obter as Trocas Realizadas', resp.message, 'error');
        }
      });
    } catch (error) {
      Swal.fire('Ocorreu um erro ao tentar obter as Trocas Realizadas', error.message, 'error');
    }
  }

  cancelar(id: string = '', seq: string = '') {
    Swal.fire({
      title: 'Cancelar',
      html: 'Deseja Cancelar a Solicitação #' + seq + '?',
      type: 'question',
      showConfirmButton: true,
      confirmButtonText: 'OK',
      showCancelButton: true
    }).then((res) => {
      if (res.value) {
        this.service.Cancelar(id)
          .subscribe((resp) => {
            if (resp.success) {
              Swal.fire('Cancelar', resp.message, 'success')
                .then((r1) => {
                  window.location.reload();
                });
            } else {
              Swal.fire('Cancelar', resp.message, 'error')
              .then((r2) => {
                window.location.reload();
              });
            }
          });
      }
    });
  }

  receber(id: string = '', seq: string = '') {
    Swal.fire({
      title: 'Confirmar o Recebimento',
      html: 'Neste momento você confirma o recebimento do(s) Produto(s) desta troca, Deseja confirmar?',
      type: 'question',
      showConfirmButton: true,
      confirmButtonText: 'OK',
      showCancelButton: true
    }).then((res) => {
      if (res.value) {
        this.service.Receber(id)
          .subscribe((resp) => {
            if (resp.success) {
              Swal.fire('Confirmado Recebimento', resp.message, 'success')
                .then((r1) => {
                  this.router.navigate(['/']);
                });
            } else {
              Swal.fire('Erro', resp.message, 'error')
              .then((r2) => {
                this.router.navigate(['/']);
              });
            }
          });
      }
    });
  }
}
