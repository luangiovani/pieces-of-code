import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { DTables } from 'src/app/_helpers/_dTables';
import { Location } from '@angular/common';
import { TrocarPontosService } from '../trocar-pontos.service';
import { ProdutosService } from '../../../admin/produtos/produtos.service';
import { AuthenticationService } from 'src/app/_services';
import { OpcoesEntregaService } from '../../../admin/opcoes-entrega/opcoes-entrega.service';
import { TaxaConversaoService } from '../../../admin/taxa-conversao/taxa-conversao.service';
import Swal from 'sweetalert2';
import * as $ from 'jquery';
import 'datatables.net';
import 'datatables.net-bs4';
import 'dropify';
import 'jquery-mask-plugin';
import * as moment from 'moment';
import { LojasService } from 'src/app/perfil/admin/lojas/lojas.service';
declare function initApp(): any;

@Component({
  selector: 'app-ver-produto',
  templateUrl: './ver-produto.component.html'
})

export class VerProdutoComponent implements OnInit {

  title: string;
  currentUser: any;
  produtoId: string;
  objProduto: any;
  taxaAtual: any;
  compraForm: FormGroup;
  opcoesEntrega: any[] = [];
  lojas: any[] = [];

  constructor(public chRef: ChangeDetectorRef,
              public router: Router,
              public location: Location,
              public fb: FormBuilder,
              public activatedroute: ActivatedRoute,
              public authService: AuthenticationService,
              public dTb: DTables,
              public produtoService: ProdutosService,
              public service: TrocarPontosService,
              public opcoesEntregaService: OpcoesEntregaService,
              public taxaService: TaxaConversaoService,
              public lojaService: LojasService) {
                activatedroute.params.subscribe(params => { this.produtoId = params.id; });
                this.title = 'Solicitação de Troca por Produto';
                this.objProduto = new Object();
                initApp();
               }

  async ngOnInit() {
    this.compraForm = this.fb.group({
      qtde: new FormControl('', [Validators.required]),
      lojaId: new FormControl('', [Validators.required]),
      opcaoEntregaId: new FormControl('', [Validators.required]),
      observacoes: new FormControl('')
    });

    this.compraForm.controls.qtde.setValue(1);

    this.lojaService.ObterLojas()
    .subscribe((resp) => {
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

    try {
      await this.taxaService.ObterAtual()
        .then((resp) => {
          if (resp.success) {
            this.taxaAtual = resp.obj;
          } else {
            Swal.fire({
              type: 'error',
              title: 'Oops...',
              text: 'Houve um erro ao tentar Obter a Taxa de Conversão cadastrada!'
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

    if (this.produtoId !== '' && this.produtoId !== undefined && this.produtoId !== null) {
      await this.produtoService.ObterProduto(this.produtoId)
        .then((resp) => {
            if (resp.success) {
              this.chRef.detectChanges();
              this.objProduto = resp.obj;
              const dataDisponibilidade = moment(this.objProduto.data_disponibilidade, 'YYYY-MM-DDTHH:mm:ss').format('DD/MM/YYYY');
              this.objProduto.dataDisponibilidade = dataDisponibilidade;
              $('.dropify').dropify({
                messages: {
                  default: 'Arraste e solte ou clique aqui para adicionar uma imagem',
                  replace: 'Arraste e solte ou clique aqui para trocar a imagem',
                  remove: 'Remover',
                  error: 'Houve um erro ao tentar adicionar uma imagem'
                },
                error: {
                    fileSize: 'O Tamanho do arquivo é muito grande (200Kb max).'
                },
                maxFileSize: '200K',
                allowedFileExtensions: ['jpg', 'png', 'gif'],
                file: this.dataURItoBlob(this.objProduto.imagem, this.objProduto.tipo_imagem)
                // defaultFile: this.objProduto.b64_imagem
                // defaultFile: this.dataURItoBlob(this.objProduto.imagem, this.objProduto.tipo_imagem)
                // defaultFile: this.objProduto.imagem + '.' + this.objProduto.ext_imagem
              });
            } else {
              Swal.fire({
                type: 'warning',
                title: 'Oops...',
                text: 'Ocorreu um erro interno.: ' + resp.message
              });
            }
        });
    } else {
      Swal.fire({
        type: 'warning',
        title: 'Oops...',
        text: 'Nenhum produto foi selecionado para Efetuar troca por pontos'
      }).then((r) => {
        this.router.navigate(['trocar-pontos']);
      });
    }
  }

  dataURItoBlob(b64Imagem: any, tipoImagem: string) {
    const byteString = window.atob(b64Imagem);
    const arrayBuffer = new ArrayBuffer(byteString.length);
    const int8Array = new Uint8Array(arrayBuffer);
    for (let i = 0; i < byteString.length; i++) {
      int8Array[i] = byteString.charCodeAt(i);
    }
    const blob = new Blob([int8Array], { type: tipoImagem });
    return blob;
 }

 obterOpcoesEntrega() {
   this.chRef.detectChanges();
   try {
      this.opcoesEntregaService.ObterOpcoesEntregaPorLoja(this.compraForm.controls.lojaId.value)
      .subscribe((resp) => {
        if (resp.success) {
          this.opcoesEntrega = resp.results;
          // $('#opcaoEntregaId').html('');
          // let sOpts = '<option value=""></option>';
          // this.opcoesEntrega.forEach(opt, i) {

          // }
          // sOpts += '<option value="' +  + '"></option>';

        } else {
          Swal.fire('Erro', resp.message, 'error');
          return;
        }
      });
    } catch (error) {
      Swal.fire('Erro', error.message, 'error');
      return;
    }
 }

 confirmar(): void {
  try {
    this.service.ConfirmarSolicitacaoTroca(this.compraForm.controls.qtde.value,
      this.compraForm.controls.opcaoEntregaId.value,
      this.compraForm.controls.lojaId.value,
      this.compraForm.controls.observacoes.value,
      this.produtoId)
      .subscribe((resp) => {
        if (resp.success) {
          Swal.fire({
            type: 'success',
            title: 'Solicitação de Troca de Produto',
            text: 'Solicitação de Troca de Produto por Pontos Realizada com Sucesso'
          }).then((r) => {
            this.router.navigate(['trocar-pontos']);
          });
        } else {
          Swal.fire({
            type: 'error',
            title: 'Solicitação de Troca de Produto',
            text: 'Ocorreu um erro ao solicitar uma troca de pontos por Produtos.: ' + resp.message
          });
        }
      });
  } catch (error) {
    Swal.fire({
      type: 'error',
      title: 'Erro ao solicitar compra',
      text: 'Ocorreu um erro.: ' + error.message
    });
  }
 }

 goback() {
  this.location.back();
 }
}
