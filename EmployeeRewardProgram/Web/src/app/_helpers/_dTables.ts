import { Injectable, OnInit, ElementRef } from '@angular/core';
import { Router } from '@angular/router';
import { Location } from '@angular/common';

import * as $ from 'jquery';
import 'datatables.net';
import 'datatables.net-bs4';

@Injectable({
    providedIn: 'root'
})

export class DTables {

    dataTable: any;

    constructor(private router: Router,
                private location: Location) { }

    montaTable(data: any[], sclass: string = '.dataTable') {
        const table: any = $(sclass);
        this.dataTable = table.DataTable({
            dom: '<"toolbar">frtip',
            responsive: true,
            pageLength: 5,
            lengthChange: false,
            dataSource: data,
            pagingType: 'full_numbers',
            // scrollX: true,
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
    }
    adiciona_botoes_header() {
        $('div.toolbar')
        .html('<button class="btn btn-primary mr-2 btnNovo">Novo</button>'
            + '<button class="btn btn-secondary btnVoltar">Voltar</button>');
    }

    ajusta_toolbar(pathnovo: string = '#', er: ElementRef, router: Router, location: Location) {
        const elNovo =  er.nativeElement.querySelector('.btnNovo');

        if (elNovo) {
            elNovo.addEventListener('click', function() {
                router.navigate([pathnovo]);
            }, true);
        }

        const elVoltar =  er.nativeElement.querySelector('.btnVoltar');

        if (elVoltar) {
            elVoltar.addEventListener('click', function() {
                location.back();
            }, true);
        }

        $('div.toolbar')
            .css('width', '50%')
                .css('float', 'left')
                    .css('margin-top', '17px');
    }

    ajusta_filter() {
        $('.dataTables_wrapper')
            .find('.dataTables_filter')
                .css('width', '50%')
                    .css('float', 'right');
    }

    ajusta_label() {
        $('.dataTables_wrapper')
            .find('.dataTables_filter').first()
                .find('label').first()
                    .css('width', '100%');
    }
}
