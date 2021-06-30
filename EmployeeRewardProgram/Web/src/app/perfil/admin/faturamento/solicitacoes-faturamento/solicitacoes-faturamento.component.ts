import { Component, OnInit, ElementRef, ChangeDetectorRef, ViewChild } from '@angular/core';
import Swal from 'sweetalert2';
import * as $ from 'jquery';
import 'datatables.net';
import 'datatables.net-bs4';
import 'dropify';
import 'jquery-mask-plugin';
import { LojasService } from 'src/app/perfil/admin/lojas/lojas.service';
import * as XLSX from 'xlsx';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/_services';
import { SituacaoCompraService } from 'src/app/perfil/admin-tec/situacao-compra/situacao-compra.service';
import { RelatoriosLojaService } from 'src/app/perfil/loja/relatorios/relatorios-loja.service';
import { environment } from 'src/environments/environment';

declare function initApp(): any;

@Component({
  selector: 'app-solicitacoes-faturamento',
  templateUrl: './solicitacoes-faturamento.component.html',
  styleUrls: ['./solicitacoes-faturamento.component.css']
})
export class SolicitacoesFaturamentoComponent implements OnInit {

  @ViewChild('TABLE', { static: false }) TABLE: ElementRef;
  title = '';
  dataSource: any[] = [];
  filtersForm: FormGroup;
  situacoes: any[] = [];
  lojas: any[] = [];

  constructor(public chRef: ChangeDetectorRef,
              public router: Router,
              public fb: FormBuilder,
              public authService: AuthenticationService,
              public situacoesCompraService: SituacaoCompraService,
              public lojaService: LojasService,
              public service: RelatoriosLojaService) {
                this.title = 'Relatório de Faturamento';
                initApp();
              }

  async ngOnInit() {
    this.filtersForm = this.fb.group({
      dataDe: new FormControl(''),
      dataAte: new FormControl(''),
      pago: new FormControl(''),
      situacaoCompraId: new FormControl(''),
      lojaId: new FormControl('')
    });

    await this.situacoesCompraService.ObterSituacoesCompraAsync()
    .then((resp) => {
      if (resp.success) {
        this.situacoes = resp.results;
      } else {
        Swal.fire({
          type: 'error',
          title: 'Oops...',
          text: 'Ocorreu um erro interno.: ' + resp.message
        });
        return;
      }
    });

    await this.lojaService.ObterLojasAsync()
      .then((resp) => {
        if (resp.success) {
          this.lojas = resp.results;
        } else {
          Swal.fire({
            type: 'error',
            title: 'Oops...',
            text: 'Ocorreu um erro interno.: ' + resp.message
          });
          return;
        }
      });

    this.chRef.detectChanges();
    $('.datepicker').datepicker({
      format: 'dd/mm/yyyy',
      autoclose: true,
      orientation: 'bottom'
    });

    this.filtersForm.controls.situacaoCompraId.setValue(environment.enumns.SituacaoCompraEnum.Finalizada);
    this.ObterRelatorio();
  }

  ObterRelatorio() {

    this.chRef.detectChanges();

    try {
      this.service.ObterRelatorioTrocas(this.filtersForm.controls.dataDe.value,
        this.filtersForm.controls.dataAte.value,
        this.filtersForm.controls.situacaoCompraId.value,
        this.filtersForm.controls.pago.value,
        this.filtersForm.controls.lojaId.value)
        .subscribe((resp) => {
          if (resp.success) {
            this.dataSource = resp.results;
          } else {
            Swal.fire({
              type: 'error',
              title: 'ERRO Relatório',
              text: 'Ocorreu um erro ao tentar obter o relatório.: ' + resp.message
            });
          }
        });
    } catch (error) {
      Swal.fire({
        type: 'error',
        title: 'ERRO Relatório',
        text: 'Ocorreu um erro ao tentar obter o relatório.: ' + error.message
      });
    }
  }

  ExportTOExcel() {
    const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(this.TABLE.nativeElement);
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'TrocasEfetuadas');
    XLSX.writeFile(wb, 'TrocasEfetuadas.xlsx');
  }

  PagarSelecionadosFaturamento() {
    const arrayCompras: any[] = [];
    $('input:checkbox[name=chkFaturar]:checked').each(function() {
      arrayCompras.push($(this).val());
    });

    this.chRef.detectChanges();

    if (arrayCompras.length <= 0) {
      Swal.fire({
        type: 'warning',
        title: 'Sem Faturamentos para Pagamento',
        text: 'É necessário informar ao menos um faturamento para realizar o pagamento!'
      });
    } else {
      try {
        this.service.EfetuarPagamentoDeComprasFauradas(arrayCompras)
          .subscribe((resp) => {
            if (resp.success) {
              Swal.fire({
                type: 'success',
                title: 'Operação realizada com Sucesso!',
                text: 'Pagamento realizado com Sucesso!'
              }).then((r) => {
                window.location.reload();
              });
            } else {
              Swal.fire({
                type: 'error',
                title: 'ERRO Relatório',
                text: 'Ocorreu um erro ao tentar pagar faturamento.: ' + resp.message
              });
            }
          });
      } catch (error) {
        Swal.fire({
          type: 'error',
          title: 'ERRO Relatório',
          text: 'Ocorreu um erro ao tentar pagar faturamento.: ' + error.message
        });
      }
    }
  }

  chkUchkAll() {
    this.chRef.detectChanges();
    $('input[type=checkbox]').prop('checked', $('input:checkbox[name=chkAll]').prop('checked'));
  }

}
