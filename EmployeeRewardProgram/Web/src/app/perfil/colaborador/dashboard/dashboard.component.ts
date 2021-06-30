import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { AuthenticationService } from 'src/app/_services';
import { DTables } from 'src/app/_helpers/_dTables';
import { DashboardService } from './dashboard.service';
import Swal from 'sweetalert2';
declare function initApp(): any;

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html'
})

export class DashboardComponent implements OnInit {

  dashboard: any;
  title: string;
  currentUser: any;
  objSaldoPontos: any;
  recomendacoesAprov: any[] = [];
  recomendacoesPends: any[] = [];
  qtdeTrocas: any[] = [];

  constructor(public chRef: ChangeDetectorRef,
              public authService: AuthenticationService,
              public dTb: DTables,
              public service: DashboardService) {
    this.title = 'Dashboard';
    this.authService.currentUser.subscribe(x => this.currentUser = x);
    initApp();
  }

  async ngOnInit() {

    this.chRef.detectChanges();

    await this.authService.getSaldoPontosGestor()
    .then((resp) => {
      if (resp.success) {
        this.objSaldoPontos = resp.obj;
      } else {
        this.objSaldoPontos.qtde_pontos = 0;
        this.objSaldoPontos.qtde_verba = 0;
      }
    });

    try {
      this.service.ObterRelatorio(this.currentUser.cs)
        .subscribe((resp) => {
          if (resp.success) {
            this.dashboard = resp.obj.extrato;
            this.recomendacoesAprov = resp.obj.recomendacoesAprovadas;
            this.recomendacoesPends = resp.obj.recomendacoesPendentes;
            this.qtdeTrocas = resp.obj.comprasRealizadas;
            this.chRef.detectChanges();
            // this.dTb.montaTable(this.recomendacoesAprov, '.dataTableRecs');
            // this.dTb.montaTable(this.recomendacoesPends, '.dataTablePends');
            this.dTb.montaTable(this.qtdeTrocas, '.dataTableTrocas');
            this.dTb.ajusta_filter();
            this.dTb.ajusta_label();
          } else {
            Swal.fire({
              type: 'error',
              title: 'Oops...',
              text: 'Houve um erro ao tentar Obter Meu Extrato de Pontos!'
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
