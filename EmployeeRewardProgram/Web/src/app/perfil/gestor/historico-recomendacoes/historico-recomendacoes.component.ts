import { Component, OnInit, ChangeDetectorRef, ElementRef } from '@angular/core';
import { DTables } from 'src/app/_helpers/_dTables';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import Swal from 'sweetalert2';
import { HistoricoRecomendacoesService } from './historico-recomendacoes.service';
import { AuthenticationService } from 'src/app/_services';
declare function initApp(): any;

@Component({
  selector: 'app-historico-recomendacoes',
  templateUrl: './historico-recomendacoes.component.html'
})

export class HistoricoRecomendacoesComponent implements OnInit {

  dataSource: any[] = [];
  title: string;
  currentUser: any;

  constructor(public elementRef: ElementRef,
              public chRef: ChangeDetectorRef,
              public router: Router,
              public location: Location,
              public authService: AuthenticationService,
              public dTb: DTables,
              public service: HistoricoRecomendacoesService) {

      this.title = 'Recomendações Realizadas';
      this.authService.currentUser.subscribe(x => this.currentUser = x);
      initApp();
   }

  async ngOnInit() {
    try {
      this.service.ObterRecomendacoes(this.currentUser.cs)
      .subscribe((resp) => {
        if (resp.success) {
          this.dataSource = resp.obj;
          this.chRef.detectChanges();
          this.dTb.montaTable(this.dataSource);
          this.dTb.ajusta_filter();
          this.dTb.ajusta_label();
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

  goToDetalhesRecomendacao(id: string = '') {
    this.router.navigate(['detalhe-recomendacao/' + id]);
  }

  goback() {
    this.location.back();
  }
}
