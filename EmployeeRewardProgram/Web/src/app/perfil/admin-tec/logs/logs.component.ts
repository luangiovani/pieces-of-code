import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import Swal from 'sweetalert2';
import { LogsService } from './logs.service';
import * as $ from 'jquery';
import 'datatables.net';
import 'datatables.net-bs4';
import 'bootstrap-datepicker';
import { FormControl, FormBuilder, FormGroup } from '@angular/forms';
declare function initApp(): any;

@Component({
  selector: 'app-logs',
  templateUrl: './logs.component.html'
})
export class LogsComponent implements OnInit {

  dataSource: any[] = [];
  dataTable: any;
  listOperacoes: any[] = [];
  title: string;
  logsForm: FormGroup;

  dtFim = new Date();
  dtIni = new Date();
  dataInicial: '';
  dataFinal: '';

  constructor(public chRef: ChangeDetectorRef,
              public fb: FormBuilder,
              public service: LogsService) {

      this.dtIni.setDate(this.dtFim.getDate() - 15);
      this.title = 'Buscar Logs.:';
      initApp();
  }

  ngOnInit() {
    this.logsForm = this.fb.group({
      dataInicial: new FormControl(''),
      dataFinal: new FormControl(''),
      operacao: new FormControl(''),
      observacoes: new FormControl('')
    });
    // this.logsForm.controls.dataInicial.setValue(GetFormattedDate(this.dtIni));
    // this.logsForm.controls.dataFinal.setValue(GetFormattedDate(this.dtFim));

    this.chRef.detectChanges();

    $('.dataInicial').datepicker({
      format: 'dd/mm/yyyy',
      autoclose: true,
      language: 'pt-BR'
    });

    $('.dataFinal').datepicker({
      format: 'dd/mm/yyyy',
      autoclose: true,
      language: 'pt-BR'
    });

    try {
      this.service.ObterListaOperacoesLogs()
        .subscribe((resp) => {
          if (resp.success) {
            this.listOperacoes = resp.results;
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

  setDtIni(value: string) {
    this.chRef.detectChanges();
    this.logsForm.controls.dataInicial.setValue(value);
  }

  setDtFim(value: string) {
    this.chRef.detectChanges();
    this.logsForm.controls.dataFinal.setValue(value);
  }

  buscarLogs() {
    this.chRef.detectChanges();
    this.service.ObterLogs(this.logsForm.controls.dataInicial.value,
                           this.logsForm.controls.dataFinal.value,
                           this.logsForm.controls.operacao.value,
                           this.logsForm.controls.observacoes.value)
    .subscribe((resp) => {
      if (resp.success) {
        this.dataSource = resp.results;
        this.chRef.detectChanges();

        const table: any = $('.dataTable');
        this.dataTable = table.DataTable({
            dom: '<"toolbar">frtip',
            responsive: true,
            destroy: true,
            pageLength: 5,
            lengthChange: false,
            data: this.dataSource,
            columns: [
              { title: '#', data: 'sequencial' },
              { title: 'Data e Hora', data: 'data_hora_inicio' },
              { title: 'Colaborador', data: 'nome_colaborador' },
              { title: 'Operação', data: 'operacao' },
              { title: 'Log', data: 'observacao' }
            ],
            pagingType: 'full_numbers',
            language: {
                sEmptyTable: 'Nenhum registro encontrado',
                sInfo: 'Mostrando de _START_ até _END_ de _TOTAL_ registros',
                sInfoEmpty: 'Mostrando 0 até 0 de 0 registros',
                sInfoFiltered: '(Filtrados de _MAX_ registros)',
                sInfoPostFix: '',
                sInfoThousands: '.',
                sLengthMenu: '_MENU_ resultados por página',
                sLoadingRecords: 'Carregando...',
                sProcessing: 'Processando...',
                sZeroRecords: 'Nenhum registro encontrado',
                sSearch: 'Filtrar',
                oPaginate: {
                    sNext: '>',
                    sPrevious: '<',
                    sFirst: '<<',
                    sLast: '>>'
                },
                oAria: {
                    sSortAscending: ': Ordenar colunas de forma ascendente',
                    sSortDescending: ': Ordenar colunas de forma descendente'
                }
            }
        });
      } else {
        Swal.fire({
          type: 'error',
          title: 'Oops...',
          text: 'Houve um erro ao tentar Obter Log\s!'
        });
      }
    });
  }
}
