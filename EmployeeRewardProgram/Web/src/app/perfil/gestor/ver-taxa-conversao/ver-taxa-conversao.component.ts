import { Component, OnInit, ChangeDetectorRef, ElementRef } from '@angular/core';
import { DTables } from 'src/app/_helpers/_dTables';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import Swal from 'sweetalert2';
import { AuthenticationService } from 'src/app/_services';
import { TaxaConversaoService } from '../../admin/taxa-conversao/taxa-conversao.service';
declare function initApp(): any;

@Component({
  selector: 'app-ver-taxa-conversao',
  templateUrl: './ver-taxa-conversao.component.html'
})

export class VerTaxaConversaoComponent implements OnInit {

  dataSource: any[] = [];
  title: string;

  constructor(public elementRef: ElementRef,
              public chRef: ChangeDetectorRef,
              public router: Router,
              public location: Location,
              public authService: AuthenticationService,
              public dTb: DTables,
              public service: TaxaConversaoService) {

      this.title = 'Taxa Conversão';
      initApp();
   }

  ngOnInit() {
    try {
      this.service.ObterTaxas()
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
              text: 'Houve um erro ao tentar Obter as Taxas cadastradas!'
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
