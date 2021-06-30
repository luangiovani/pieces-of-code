import { Component, OnInit, ElementRef, ChangeDetectorRef } from '@angular/core';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { ProdutosService } from '../produtos.service';
import * as $ from 'jquery';
import 'datatables.net';
import 'datatables.net-bs4';
import 'dropify';
import 'jquery-mask-plugin';
import * as moment from 'moment';
import Swal from 'sweetalert2';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { TaxaConversaoService } from '../../taxa-conversao/taxa-conversao.service';
declare function initApp(): any;

@Component({
  selector: 'app-produtos-novo',
  templateUrl: './produtos-novo.component.html'
})

export class ProdutosNovoComponent implements OnInit {

  produtoId: string;
  produtoForm: FormGroup;
  isLoading = false;
  objProduto: any;
  taxaAtual: any;
  fileToUpload: File = null;
  title: string;
  currentUser: any;

  constructor(public elementRef: ElementRef,
              public chRef: ChangeDetectorRef,
              public router: Router,
              public location: Location,
              public authService: AuthenticationService,
              public service: ProdutosService,
              public taxaService: TaxaConversaoService,
              public fb: FormBuilder,
              public activatedroute: ActivatedRoute) {

    activatedroute.params.subscribe(params => { this.produtoId = params.produtoId; });
    if (this.produtoId === '' || this.produtoId === undefined || this.produtoId === null) {
      this.title = 'Cadastro de Novo Produto';
    } else {
      this.title = 'Edição de Produto';
    }
    this.authService.currentUser.subscribe(x => this.currentUser = x);
    this.objProduto = new Object();
    initApp();
  }

  async ngOnInit() {
    this.produtoForm = this.fb.group({
      id: new FormControl(''),
      ativo: new FormControl(''),
      b64Imagem: new FormControl(''),
      disponivel: new FormControl(''),
      dataDisponibilidade: new FormControl(''),
      valorPontos: new FormControl('', [Validators.required]),
      valorMonetario: new FormControl(''),
      nome: new FormControl('', [Validators.required]),
      descricao: new FormControl('', [Validators.required]),
      observacoes: new FormControl('')
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
      await this.service.ObterProduto(this.produtoId)
        .then((resp) => {
            if (resp.success) {
              this.chRef.detectChanges();
              this.objProduto = resp.obj;
              this.produtoForm.controls.id.setValue(this.objProduto.id);
              this.produtoForm.controls.ativo.setValue(this.objProduto.ativo);
              this.produtoForm.controls.disponivel.setValue(this.objProduto.disponibilidade ? 'Sim' : 'Não');
              const dataDisponibilidade = moment(this.objProduto.data_disponibilidade, 'YYYY-MM-DDTHH:mm:ss').format('DD/MM/YYYY');
              this.produtoForm.controls.dataDisponibilidade.setValue(dataDisponibilidade);
              this.objProduto.dataDisponibilidade = dataDisponibilidade;
              this.produtoForm.controls.valorPontos.setValue(this.objProduto.valor_pontos);
              this.produtoForm.controls.valorMonetario.setValue(this.objProduto.valor_monetario);
              this.produtoForm.controls.nome.setValue(this.objProduto.nome);
              if (this.objProduto.descricao !== undefined &&
                  this.objProduto.descricao !== null &&
                  this.objProduto.descricao !== '') {
                    this.objProduto.descricao = this.objProduto.descricao.replace(/\\r\\n/g, '');
                    this.produtoForm.controls.descricao.setValue(this.objProduto.descricao);
                  }
              if (this.objProduto.observacoes !== undefined &&
                this.objProduto.observacoes !== null &&
                this.objProduto.observacoes !== '') {
                  this.objProduto.observacoes = this.objProduto.observacoes.replace(/\\r\\n/g, '');
                  this.produtoForm.controls.observacoes.setValue(this.objProduto.observacoes);
                }
              this.produtoForm.controls.b64Imagem.setValue(this.objProduto.imagem);
              this.setPontos(this.objProduto.valor_monetario);
              this.setValor(this.objProduto.valor_pontos);
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
              this.objProduto.id = '';
              this.objProduto.ativo = 'true';
              this.objProduto.b64_imagem = '';
              this.objProduto.tipo_imagem = '';
              this.objProduto.nome_imagem = '';
              this.objProduto.disponibilidade = '';
              this.objProduto.data_disponibilidade = '';
              this.objProduto.dataDisponibilidade = moment().format('DD/MM/YYYY');
              this.produtoForm.controls.dataDisponibilidade.setValue(moment().format('DD/MM/YYYY'));
              this.objProduto.valor_pontos = '';
              this.objProduto.valor_monetario = '';
              this.objProduto.nome = '';
              this.objProduto.descricao = '';
              this.objProduto.observacoes = '';
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
                allowedFileExtensions: ['jpg', 'png', 'gif']
              });
            }
        });
    } else {
      this.objProduto.id = '';
      this.objProduto.ativo = 'true';
      this.objProduto.b64_imagem = '';
      this.objProduto.tipo_imagem = '';
      this.objProduto.nome_imagem = '';
      this.objProduto.disponibilidade = '';
      this.objProduto.data_disponibilidade = '';
      this.objProduto.dataDisponibilidade = moment().format('DD/MM/YYYY');
      this.produtoForm.controls.dataDisponibilidade.setValue(moment().format('DD/MM/YYYY'));
      this.objProduto.valor_pontos = '';
      this.objProduto.valor_monetario = '';
      this.objProduto.nome = '';
      this.objProduto.descricao = '';
      this.objProduto.observacoes = '';
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
        allowedFileExtensions: ['jpg', 'png', 'gif']
      });
    }

    this.chRef.detectChanges();
    $('.datepicker').datepicker({
      format: 'dd/mm/yyyy',
      autoclose: true,
      orientation: 'bottom'
    });

    $('.valorMonetario').mask('#.##0,00', { reverse: true });
  }

  confirmar(): void {

    Swal.fire({
      title: 'Confirmar Operação',
      html: 'Confirmar ' + this.title + '?',
      type: 'question',
      showCancelButton: true,
      confirmButtonText: 'Confirmar',
      cancelButtonText: 'Cancelar'
    })
    .then((result) => {
      if (result.value) {

        this.objProduto.valor_pontos = this.produtoForm.controls.valorPontos.value;
        this.objProduto.valor_pontos = this.objProduto.valor_pontos.replace('.', '');
        this.objProduto.valor_pontos = this.objProduto.valor_pontos.replace(',', '.');

        this.objProduto.valor_monetario = this.produtoForm.controls.valorMonetario.value;
        this.objProduto.valor_monetario = this.objProduto.valor_monetario.replace('.', '');
        this.objProduto.valor_monetario = this.objProduto.valor_monetario.replace(',', '.');

        this.service.ConfirmarProduto(this.produtoForm.controls.id.value,
                                      this.produtoForm.controls.ativo.value,
                                      this.objProduto.b64_imagem,
                                      this.produtoForm.controls.disponivel.value,
                                      this.produtoForm.controls.dataDisponibilidade.value,
                                      this.objProduto.valor_pontos,
                                      this.objProduto.valor_monetario,
                                      this.produtoForm.controls.nome.value,
                                      this.produtoForm.controls.descricao.value,
                                      this.produtoForm.controls.observacoes.value,
                                      this.currentUser.cs)
          .subscribe((recModel) => {
            if (recModel.success) {
              Swal.fire({
                title: 'Operação realizada!',
                html: 'Produto gravado no banco de dados',
                type: 'success',
                confirmButtonText: 'OK'
              })
              .then((res) => {
                this.router.navigate(['produtos']);
                // window.location.href = 'aplicacao';
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

  goback() {
    this.location.back();
  }

  setDate(value: string) {
    this.chRef.detectChanges();
    this.produtoForm.controls.dataDisponibilidade.setValue(value);
  }

  setPontos(value: any) {
    if (value !== undefined && value !== null && value !== '') {
      if (typeof(value) === 'string') {
        value = value.replace('.', '');
        value = value.replace(',', '.');
        value = parseFloat(value).toFixed(2);
      }
      this.chRef.detectChanges();
      if (this.taxaAtual === undefined || this.taxaAtual === null) {
        value = value / 1000;
      } else {
        value = value / this.taxaAtual.taxa;
      }
      this.produtoForm.controls.valorPontos.setValue(value.toFixed(0));
    }
  }

  setValor(value: any) {
    if (value !== undefined && value !== null && value !== '') {
      if (typeof(value) === 'string') {
        value = value.replace('.', '');
        value = value.replace(',', '.');
        value = parseFloat(value).toFixed(2);
      }
      this.chRef.detectChanges();
      if (this.taxaAtual === undefined || this.taxaAtual === null) {
        value = (value * 1000).toLocaleString('pt-BR', { minimumFractionDigits: 2 });
      } else {
        value = (value * this.taxaAtual.taxa).toLocaleString('pt-BR', { minimumFractionDigits: 2 });
      }
      this.produtoForm.controls.valorMonetario.setValue(value);
      $('.valorMonetario').mask('#.##0,00', { reverse: true });
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

  upImagem(event: any) {
    const fileList: FileList = event.target.files;
    if (fileList.length > 0) {
      this.fileToUpload = fileList[0];
      this.handleInputChange(this.fileToUpload);
    }
  }

  handleInputChange(files: any) {
    const file = files;
    const pattern = /image-*/;
    const reader = new FileReader();
    if (!file.type.match(pattern)) {
      Swal.fire({
        title: 'Erro Imagem!',
        html: 'Formato inválido para Imagem!',
        type: 'error',
        confirmButtonText: 'OK'
      });
      return;
    }
    reader.onloadend = this._handleReaderLoaded.bind(this);
    reader.readAsDataURL(file);
  }

  _handleReaderLoaded(e: any) {
    const reader = e.target;
    this.objProduto.b64_imagem = 'data:' + this.fileToUpload.type + ';base64,' + reader.result.substr(reader.result.indexOf(',') + 1);
  }
}
