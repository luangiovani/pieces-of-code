import { Component, OnInit } from '@angular/core';
declare function initApp(): any;
@Component({
  selector: 'app-opcoes-valores',
  templateUrl: './opcoes-valores.component.html'
})
export class OpcoesValoresComponent implements OnInit {

  constructor() {
    initApp();
  }

  ngOnInit() { }

}
