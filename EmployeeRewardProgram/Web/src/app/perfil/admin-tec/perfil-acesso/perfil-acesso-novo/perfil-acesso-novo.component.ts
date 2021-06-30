import { Component, OnInit, ElementRef, ChangeDetectorRef } from '@angular/core';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { PerfilAcessoService } from '../perfil-acesso.service';
import { MenusService } from '../../menus/menus.service';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import Swal from 'sweetalert2';
import '../../../../../assets/libs/multiselect/jquery.multi-select.js';
declare function initApp(): any;
let listMenusSelected = new Array();

@Component({
  selector: 'app-perfil-acesso-novo',
  templateUrl: './perfil-acesso-novo.component.html'
})

export class PerfilAcessoNovoComponent implements OnInit {

  perfilId: string;
  perfilForm: FormGroup;
  isLoading = false;
  objPerfil: any;
  listMenus: any[] = [];
  title: string;
  currentUser: any;

  constructor(public elementRef: ElementRef,
              public chRef: ChangeDetectorRef,
              public router: Router,
              public location: Location,
              public authService: AuthenticationService,
              public service: PerfilAcessoService,
              public menuService: MenusService,
              public fb: FormBuilder,
              public activatedroute: ActivatedRoute) {

    activatedroute.params.subscribe(params => { this.perfilId = params.perfilId; });
    if (this.perfilId === '' || this.perfilId === undefined || this.perfilId === null) {
      this.perfilId = '';
      this.title = 'Cadastro de Novo Perfil';
    } else {
      this.title = 'Edição de Perfil';
    }
    this.authService.currentUser.subscribe(x => this.currentUser = x);
    this.objPerfil = new Object();
    initApp();
  }

  ngOnInit() {
    this.perfilForm = this.fb.group({
      id: new FormControl(''),
      nome: new FormControl('', [Validators.required]),
      descricao: new FormControl(''),
      listaPermissoesMenu: new FormControl(''),
      ativo: new FormControl('')
    });

    try {
      if (this.perfilId !== '' && this.perfilId !== undefined && this.perfilId !== null) {
        this.service.ObterPerfil(this.perfilId)
          .subscribe((resp) => {
              if (resp.success) {
                this.chRef.detectChanges();
                this.objPerfil = resp.obj;
                this.perfilForm.controls.id.setValue(this.objPerfil.id);
                this.perfilForm.controls.nome.setValue(this.objPerfil.nome);
                this.perfilForm.controls.descricao.setValue(this.objPerfil.descricao);
                this.perfilForm.controls.ativo.setValue(this.objPerfil.ativo);
                listMenusSelected = resp.obj.listaMenus;
              } else {
                this.objPerfil.id = '';
                this.objPerfil.nome = '';
                this.objPerfil.descricao = '';
                this.objPerfil.ativo = 'true';
                this.objPerfil.listaPermissoesMenu = [];
                listMenusSelected = [];
                Swal.fire({
                  type: 'error',
                  title: 'Oops...',
                  text: 'Houve um erro ao tentar Obter o perfil cadastrado!'
                });
              }
          });
      } else {
        this.objPerfil.id = '';
        this.objPerfil.nome = '';
        this.objPerfil.descricao = '';
        this.objPerfil.ativo = 'true';
        this.objPerfil.listaPermissoesMenu = [];
      }
    } catch (err) {
      Swal.fire({
        type: 'warning',
        title: 'Oops...',
        text: 'Ocorreu um erro interno.: ' + err.message
      });
    }

    try {
      this.menuService.ObterMenus()
        .subscribe((resp) => {
          if (resp.success) {
            this.listMenus = resp.results;
            this.chRef.detectChanges();
            $('[data-plugin="multiselect"]').multiSelect({
              keepOrder: true,
              afterSelect(id) {
                // const opts = Array.from(this.perfilForm.controls.listaPermissoesMenu.value);
                // opts.push(id[0]);
                // this.perfilForm.controls.listaPermissoesMenu.setValue(opts.join(','));
                listMenusSelected.push(id[0]);
              },
              afterDeselect(id) {
                // let opts = Array.from(this.perfilForm.controls.listaPermissoesMenu.value);
                // const index = opts.indexOf(id[0]);
                // opts = opts.splice(index, 1);
                // this.perfilForm.controls.listaPermissoesMenu.setValue(opts.join(','));
                const index = listMenusSelected.indexOf(id[0]);
                listMenusSelected.splice(index, 1);
              }
            });
            this.chRef.detectChanges();
            $('[data-plugin="multiselect"]').multiSelect('select', listMenusSelected);
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
        // const menus = Array.from(this.perfilForm.controls.listaPermissoesMenu.value);
        this.service.ConfirmarPerfil(
          this.perfilForm.controls.id.value,
          this.perfilForm.controls.nome.value,
          this.perfilForm.controls.descricao.value,
          this.perfilForm.controls.ativo.value,
          listMenusSelected,
          this.currentUser.cs)
          .subscribe((recModel) => {
            if (recModel.success) {
              Swal.fire({
                title: 'Operação realizada!',
                html: 'Perfil gravado no banco de dados',
                type: 'success',
                confirmButtonText: 'OK'
              })
              .then((res) => {
                this.router.navigate(['perfil-acesso']);
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
