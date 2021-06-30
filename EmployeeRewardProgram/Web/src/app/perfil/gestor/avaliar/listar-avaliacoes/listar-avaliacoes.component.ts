import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { DTables } from 'src/app/_helpers/_dTables';
import { AvaliarService } from '../avaliar.service';
import Swal from 'sweetalert2';
declare function initApp(): any;

@Component({
  selector: 'app-listar-avaliacoes',
  templateUrl: './listar-avaliacoes.component.html'
})

export class ListarAvaliacoesComponent implements OnInit {

  dataSource: any[] = [];
  title: string;

  constructor(public chRef: ChangeDetectorRef,
              public dTb: DTables,
              public service: AvaliarService) {
    this.title = 'Avaliações Realizadas';
    initApp();
  }

  ngOnInit() {
    try {
      this.service.ObterAvaliacoesRealizadas()
      .subscribe((resp) => {
        if (resp.success) {
          this.chRef.detectChanges();
          this.dataSource = resp.results;
          this.dTb.montaTable(this.dataSource);
          this.dTb.ajusta_filter();
          this.dTb.ajusta_label();
        } else {
          Swal.fire('Ocorreu um erro ao tentar obter as Avaliações Realizadas', resp.message, 'error');
        }
      });
    } catch (error) {
      Swal.fire('Ocorreu um erro ao tentar obter as Avaliações Realizadas', error.message, 'error');
    }
  }
}
