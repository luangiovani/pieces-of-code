import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { SolicitacoesService } from './solicitacoes.service';
import { DTables } from 'src/app/_helpers/_dTables';
import { AuthenticationService } from 'src/app/_services';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
declare function initApp(): any;

@Component({
  selector: 'app-solicitacoes',
  templateUrl: './solicitacoes.component.html'
})
export class SolicitacoesComponent implements OnInit {

  objTrocas: any;
  dataSource: any[] = [];
  title: string;

  constructor(public chRef: ChangeDetectorRef,
              public router: Router,
              public authService: AuthenticationService,
              public dTb: DTables,
              public service: SolicitacoesService) {
                this.title = 'Trocas Pendendes';
                initApp();
              }

  ngOnInit() {
    try {
      this.service.ObterSolicitacoesPendentes()
        .subscribe((resp) => {
          if (resp.success) {
            this.objTrocas = resp.obj;
            this.dataSource = resp.obj.trocasPendentes;
            this.chRef.detectChanges();
            this.dTb.montaTable(this.dataSource);
            this.dTb.ajusta_filter();
            this.dTb.ajusta_label();
          } else {
            Swal.fire({
              type: 'error',
              title: 'Oops...',
              text: 'Houve um erro ao tentar Obter as Trocas Pendentes.: ' + resp.message + '!'
            });
          }
        });
    } catch (error) {
      Swal.fire({
        type: 'error',
        title: 'Oops...',
        text: 'Houve um erro ao tentar Obter as Trocas Pendentes.: ' + error.message + '!'
      });
    }
  }

  detalhe(id: string = '') {
      this.router.navigate(['detalhe-solicitacao/' + id]);
  }
}
