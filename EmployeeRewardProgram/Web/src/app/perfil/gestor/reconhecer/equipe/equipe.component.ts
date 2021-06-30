import { Component, OnInit, ChangeDetectorRef, ElementRef } from '@angular/core';
import { DTables } from 'src/app/_helpers/_dTables';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import Swal from 'sweetalert2';
import { EquipeService } from './equipe.service';
import { ColaboradorService } from '../../../admin/colaboradores/colaborador.service';
import { AuthenticationService } from 'src/app/_services/authentication.service';
declare function initApp(): any;

@Component({
  selector: 'app-equipe',
  templateUrl: './equipe.component.html'
})

export class EquipeComponent implements OnInit {

  dataSource: any[] = [];
  title: string;
  currentUser: any;
  objSaldoPontos: any;

  constructor(public elementRef: ElementRef,
              public chRef: ChangeDetectorRef,
              public router: Router,
              public location: Location,
              public authService: AuthenticationService,
              public dTb: DTables,
              public service: EquipeService,
              public colaboradorService: ColaboradorService) {

      this.title = 'Colaboradores da Minha Equipe';
      this.authService.currentUser.subscribe(x => this.currentUser = x);
      this.objSaldoPontos = new Object();
      /// Quantidade de Pontos de Verba do Gestor
      this.objSaldoPontos.qtde_verba = 0;
      /// Quantidade de Pontos de Saldo do Colaborador
      this.objSaldoPontos.qtde_pontos = 0;
      initApp();
   }

  async ngOnInit() {
    await this.authService.getSaldoPontosGestor()
    .then((resp) => {
      if (resp.success) {
        this.objSaldoPontos = resp.obj;
      } else {
        this.objSaldoPontos.qtde_pontos = 0;
        this.objSaldoPontos.qtde_verba = 0;
      }
    });

    try {
      await this.colaboradorService.ListarColaboradores(this.currentUser.cs, true)
      .then((resp) => {
        if (resp.success) {
          this.dataSource = resp.results;
          this.chRef.detectChanges();
          this.dTb.montaTable(this.dataSource);
          this.dTb.ajusta_filter();
          this.dTb.ajusta_label();
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

  reconhecer(cs: string, nome: string) {
    Swal.fire({
      title: 'Confirmar Operação',
      html: 'Deseja realizar um reconhecimento para o colaborador' + cs + ' - ' + nome + '?',
      type: 'question',
      showCancelButton: true,
      confirmButtonText: 'Confirmar',
      cancelButtonText: 'Cancelar'
    })
    .then((result) => {
      if (result.value) {
        // this.router.navigate(['reconhecer', { cs, meutime: true }]);
        this.router.navigate(['reconhecer/' + cs + '/true']);
      }
    });
  }

  goback() {
    this.location.back();
  }
}
