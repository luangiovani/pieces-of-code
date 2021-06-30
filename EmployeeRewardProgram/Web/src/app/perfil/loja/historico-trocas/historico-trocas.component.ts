import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/_services';
import { DTables } from 'src/app/_helpers/_dTables';
import { HistoricoTrocasService } from './historico-trocas.service';
import Swal from 'sweetalert2';
declare function initApp(): any;

@Component({
  selector: 'app-historico-trocas',
  templateUrl: './historico-trocas.component.html'
})
export class HistoricoTrocasComponent implements OnInit {

  title = '';
  dataSource: any[] = [];

  constructor(public chRef: ChangeDetectorRef,
              public router: Router,
              public authService: AuthenticationService,
              public dTb: DTables,
              public service: HistoricoTrocasService) {
                this.title = 'Histórico de Trocas';
                initApp();
              }

  ngOnInit() {
    try {
      this.service.ObterHistorico()
        .subscribe((resp) => {
          if (resp.success) {
            this.dataSource = resp.results;
            this.chRef.detectChanges();
            this.dTb.montaTable(this.dataSource);
            this.dTb.ajusta_filter();
            this.dTb.ajusta_label();
          } else {
            Swal.fire({
              type: 'error',
              title: 'Oops...',
              text: 'Houve um erro ao tentar Obter o Histórico!'
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
}
