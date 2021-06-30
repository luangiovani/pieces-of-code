import { Component, OnInit, ElementRef, ChangeDetectorRef } from '@angular/core';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import { AuthenticationService } from 'src/app/_services';
import { DTables } from 'src/app/_helpers/_dTables';
import { LojasService } from '../../admin/lojas/lojas.service';
import Swal from 'sweetalert2';
declare function initApp(): any;

@Component({
  selector: 'app-usuario-loja',
  templateUrl: './usuario-loja.component.html',
  styleUrls: ['./usuario-loja.component.css']
})

export class UsuarioLojaComponent implements OnInit {

  dataSource: any[] = [];
  title: string;

  constructor(public elementRef: ElementRef,
              public chRef: ChangeDetectorRef,
              public router: Router,
              public location: Location,
              public authService: AuthenticationService,
              public dTb: DTables,
              public service: LojasService) {

      this.title = 'Employee users from Shop';
      initApp();
   }

  ngOnInit() {
    try {
      this.service.ObterColaboradoresLoja()
        .subscribe((resp) => {
          if (resp.success) {
            this.dataSource = resp.results.filter((r) => {
              return (r.loja_id !== '' && r.loja_id !== null && r.loja_id !== undefined);
            });
            this.chRef.detectChanges();
            this.dTb.montaTable(this.dataSource);
            this.chRef.detectChanges();
            this.dTb.adiciona_botoes_header();
            this.chRef.detectChanges();
            this.dTb.ajusta_toolbar('cadastro-usuario-loja/', this.elementRef, this.router, this.location);
            this.chRef.detectChanges();
            this.dTb.ajusta_filter();
            this.chRef.detectChanges();
            this.dTb.ajusta_label();
          } else {
            Swal.fire({
              type: 'error',
              title: 'Oops...',
              text: 'Houve um erro ao tentar Obter os Colaboradores com Perfil loja cadastrados!'
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

  editar(cs: string, lojaId: string) {
    this.router.navigate(['cadastro-usuario-loja/' + cs + '/' + lojaId]);
  }

  desvincular(id: string, nome: string, loja: string) {

    Swal.fire({
      title: 'Confirmar Operação',
      html: 'Deseja desvincular o Colaborador.: ' + nome + ' com a Loja.: ' + loja + '?',
      type: 'question',
      showCancelButton: true,
      confirmButtonText: 'Confirmar',
      cancelButtonText: 'Cancelar'
    })
    .then((result) => {
      if (result.value) {
        try {
          this.service.DesvincularColaboradorLoja(id)
          .subscribe((resp) => {
            if (resp.success) {
              Swal.fire({
                type: 'success',
                title: 'Operação realizada com sucesso!',
                text: 'O Vínculo foi removido.'
              }).then((r) => {
                window.location.reload();
              });
            } else {
              Swal.fire({
                type: 'error',
                title: 'Oops...',
                text: 'Ocorreu um erro interno.: ' + resp.message
              });
            }
          });
        } catch (err) {
          Swal.fire({
            type: 'error',
            title: 'Oops...',
            text: 'Ocorreu um erro interno.: ' + err.message
          });
        }
      }
    });
  }
}
