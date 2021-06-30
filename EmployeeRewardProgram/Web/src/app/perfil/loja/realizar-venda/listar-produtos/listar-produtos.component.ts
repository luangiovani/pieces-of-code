import { Component, OnInit, ChangeDetectorRef, ElementRef } from '@angular/core';
import { DTables } from 'src/app/_helpers/_dTables';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import Swal from 'sweetalert2';
import { AuthenticationService } from 'src/app/_services';
import { TrocarPontosService } from '../../../colaborador/trocar-pontos/trocar-pontos.service';
import { RealizarVendaService } from '../realizar-venda.service';

declare function initApp(): any;

@Component({
  selector: 'app-listar-produtos',
  templateUrl: './listar-produtos.component.html',
  styleUrls: ['./listar-produtos.component.css']
})
export class ListarProdutosComponent implements OnInit {

  dataSource: any[] = [];
  title: string;
  currentUser: any;
  cscolaborador: string;
  objColaborador: any;

  constructor(public chRef: ChangeDetectorRef,
              public router: Router,
              public location: Location,
              public activatedroute: ActivatedRoute,
              public dTb: DTables,
              public authService: AuthenticationService,
              public service: RealizarVendaService,
              public trocaService: TrocarPontosService) {
                activatedroute.params.subscribe(params => { this.cscolaborador = params.cs; });
                this.title = 'Listagem de Produtos para Troca';
                this.objColaborador = new Object();
                this.authService.currentUser.subscribe(x => this.currentUser = x);
                initApp();
              }

  async ngOnInit() {

    this.chRef.detectChanges();
    try {
      this.service.ObterColaborador(this.cscolaborador)
        .subscribe((resp) => {
          if (resp.success) {
            this.objColaborador = resp.obj;
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

    try {
      this.trocaService.ObterProdutos()
      .subscribe((resp) => {
        if (resp.success) {
          this.dataSource = resp.results;
          this.chRef.detectChanges();
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

  solicitar(id: string = '', produtoPontos: number = 0) {
    if (this.objColaborador.quantidade_pontos >= produtoPontos) {
      this.router.navigate(['efetivar-venda/' + this.cscolaborador + '/' + id]);
    } else {
      Swal.fire({
        type: 'error',
        title: 'Oops...',
        text: 'O Produto não pode ser selecionado, saldo insuficiente de pontos'
      });
    }
  }
}
