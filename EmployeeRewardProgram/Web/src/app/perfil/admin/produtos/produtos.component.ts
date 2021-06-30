import { Component, OnInit, ChangeDetectorRef, ElementRef } from '@angular/core';
import { DTables } from 'src/app/_helpers/_dTables';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import Swal from 'sweetalert2';
import { ProdutosService } from './produtos.service';
import { AuthenticationService } from 'src/app/_services/authentication.service';
declare function initApp(): any;
@Component({
  selector: 'app-produtos',
  templateUrl: './produtos.component.html'
})

export class ProdutosComponent implements OnInit {

  dataSource: any[] = [];
  title: string;

  constructor(public elementRef: ElementRef,
              public chRef: ChangeDetectorRef,
              public router: Router,
              public location: Location,
              public authService: AuthenticationService,
              public dTb: DTables,
              public service: ProdutosService) {

      this.title = 'Produtos';
      initApp();
   }

  ngOnInit() {
    try {
      this.service.ObterProdutos()
        .subscribe((resp) => {
          if (resp.success) {
            this.dataSource = resp.results;
            this.chRef.detectChanges();
            this.dTb.montaTable(this.dataSource);
            this.chRef.detectChanges();
            this.dTb.adiciona_botoes_header();
            this.chRef.detectChanges();
            this.dTb.ajusta_toolbar('produtos-novo/', this.elementRef, this.router, this.location);
            this.chRef.detectChanges();
            this.dTb.ajusta_filter();
            this.chRef.detectChanges();
            this.dTb.ajusta_label();
          } else {
            Swal.fire({
              type: 'error',
              title: 'Oops...',
              text: 'Houve um erro ao tentar Obter os Produtos cadastrados!'
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

  situacao(id: string = '', nome: string = '', ativo: string = 'true') {
    const bAtivo = ativo === 'true';
    Swal.fire({
      title: 'Confirmar Operação',
      html: 'Deseja ' + (bAtivo ? 'ativar' : 'inativar' ) + ' o produto <b>' + nome + '</b>?',
      type: 'question',
      showCancelButton: true,
      confirmButtonText: 'Confirmar',
      cancelButtonText: 'Cancelar'
    })
    .then((result) => {
      if (result.value) {
        this.service.AtivarInativar(
          id,
          ativo)
          .subscribe((recModel) => {
            if (recModel.success) {
              Swal.fire({
                title: 'Operação realizada!',
                html: 'Produto gravado no banco de dados',
                type: 'success',
                confirmButtonText: 'OK'
              })
              .then((res) => {
                window.location.href = 'produtos';
              });
            } else {
              Swal.fire({
                title: 'Operação não realizada!',
                html: recModel.message,
                type: 'error',
                confirmButtonText: 'OK'
              });
            }
          });
      }
    });
  }

  editar(id: string) {
    this.router.navigate(['produtos-novo/', id]);
  }
}
