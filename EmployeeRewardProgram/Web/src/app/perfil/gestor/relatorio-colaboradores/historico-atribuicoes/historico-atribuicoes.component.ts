import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { Router } from '@angular/router';
import { DTables } from 'src/app/_helpers/_dTables';
import { HistoricoAtribuicoesService } from './historico-atribuicoes.service';
import Swal from 'sweetalert2';
declare function initApp(): any;

@Component({
  selector: 'app-historico-atribuicoes',
  templateUrl: './historico-atribuicoes.component.html'
})
export class RelatorioHistoricoAtribuicoesComponent implements OnInit {

  dataSource: any[];
  title: string;

  constructor(public chRef: ChangeDetectorRef,
              public router: Router,
              public dTb: DTables,
              public service: HistoricoAtribuicoesService) {
                this.title = 'Histórico de Recomendações Aprovadas de Colaboradores';
                initApp();
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
            Swal.fire('Ocorreu um Erro', resp.message, 'error');
          }
        });
    } catch (error) {
      Swal.fire('Ocorreu um Erro', error.message, 'error');
    }
  }

}
