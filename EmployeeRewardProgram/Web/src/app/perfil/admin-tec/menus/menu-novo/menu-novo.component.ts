import { Component, OnInit, ElementRef, ChangeDetectorRef } from '@angular/core';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { MenusService } from '../menus.service';
import { AplicacaoService } from '../../aplicacao/aplicacao.service';
import Swal from 'sweetalert2';
import { AuthenticationService } from 'src/app/_services/authentication.service';
declare function initApp(): any;

@Component({
  selector: 'app-menu-novo',
  templateUrl: './menu-novo.component.html'
})

export class MenuNovoComponent implements OnInit {

  menuId: string;
  menuForm: FormGroup;
  isLoading = false;
  objMenu: any;
  listMenus: any[] = [];
  listAplicacoes: any[] = [];
  title: string;
  currentUser: any;
  constructor(public elementRef: ElementRef,
              public chRef: ChangeDetectorRef,
              public router: Router,
              public location: Location,
              public authService: AuthenticationService,
              public service: MenusService,
              public aplicacaoService: AplicacaoService,
              public fb: FormBuilder,
              public activatedroute: ActivatedRoute) {
    activatedroute.params.subscribe(params => { this.menuId = params.menuId; });
    if (this.menuId === '' || this.menuId === undefined || this.menuId === null) {
      this.menuId = '';
      this.title = 'Cadastro de Nova Opção de Menu';
    } else {
      this.title = 'Edição de Opção de Menu';
    }
    this.authService.currentUser.subscribe(x => this.currentUser = x);
    this.objMenu = new Object();
    initApp();
  }

  ngOnInit() {
    this.menuForm = this.fb.group({
      id: new FormControl(''),
      aplicacao_id: new FormControl('', [Validators.required]),
      menu_superior_id: new FormControl(''),
      nome: new FormControl('', [Validators.required]),
      controller: new FormControl(''),
      acao: new FormControl(''),
      icone: new FormControl(''),
      ativo: new FormControl('')
    });

    try {
      this.service.ObterMenus()
        .subscribe((resp) => {
          if (resp.success) {
            this.listMenus = resp.results;
          } else {
            Swal.fire({
              type: 'error',
              title: 'Oops...',
              text: 'Houve um erro ao tentar Obter as Opções de Menu cadastradas!'
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
      this.aplicacaoService.ObterAplicacoes()
        .subscribe((resp) => {
          if (resp.success) {
            this.listAplicacoes = resp.results.filter(apls => apls.ativo);
          } else {
            Swal.fire({
              type: 'error',
              title: 'Oops...',
              text: 'Houve um erro ao tentar Obter as Aplicações cadastradas!'
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
      if (this.menuId !== '' && this.menuId !== undefined && this.menuId !== null) {
        this.service.ObterMenu(this.menuId)
          .subscribe((resp) => {
              if (resp.success) {
                this.chRef.detectChanges();
                this.objMenu = resp.obj;
                this.menuForm.controls.id.setValue(this.objMenu.id);
                this.menuForm.controls.aplicacao_id.setValue(this.objMenu.aplicacao_id);
                this.menuForm.controls.menu_superior_id.setValue(this.objMenu.menu_superior_id);
                this.menuForm.controls.nome.setValue(this.objMenu.nome);
                this.menuForm.controls.controller.setValue(this.objMenu.controller);
                this.menuForm.controls.acao.setValue(this.objMenu.acao);
                this.menuForm.controls.icone.setValue(this.objMenu.icone);
                this.menuForm.controls.ativo.setValue(this.objMenu.ativo);
              } else {
                this.objMenu.id = '';
                this.objMenu.aplicacao_id = '';
                this.objMenu.menu_superior_id = '';
                this.objMenu.nome = '';
                this.objMenu.controller = '';
                this.objMenu.acao = '';
                this.objMenu.icone = '';
                this.objMenu.ativo = 'true';

                Swal.fire({
                  type: 'error',
                  title: 'Oops...',
                  text: 'Houve um erro ao tentar Obter a opção de menu cadastrada!'
                });
              }
          });
      } else {
        this.objMenu.id = '';
        this.objMenu.aplicacao_id = '';
        this.objMenu.menu_superior_id = '';
        this.objMenu.nome = '';
        this.objMenu.controller = '';
        this.objMenu.acao = '';
        this.objMenu.icone = '';
        this.objMenu.ativo = 'true';
      }
    } catch (err) {
      Swal.fire({
        type: 'warning',
        title: 'Oops...',
        text: 'Ocorreu um erro interno.: ' + err.message
      });
    }
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
        this.service.ConfirmarMenu(
          this.menuForm.controls.id.value,
          this.menuForm.controls.aplicacao_id.value,
          this.menuForm.controls.menu_superior_id.value,
          this.menuForm.controls.nome.value,
          this.menuForm.controls.controller.value,
          this.menuForm.controls.acao.value,
          this.menuForm.controls.icone.value,
          this.menuForm.controls.ativo.value,
          this.currentUser.cs)
          .subscribe((recModel) => {
            if (recModel.success) {
              Swal.fire({
                title: 'Operação realizada!',
                html: 'Opção de Menu gravada no banco de dados',
                type: 'success',
                confirmButtonText: 'OK'
              })
              .then((res) => {
                this.router.navigate(['menus']);
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

}
