import { Component, OnInit } from '@angular/core';
declare function initApp(): any;
@Component({
  selector: 'app-opcoes-valores-novo',
  templateUrl: './opcoes-valores-novo.component.html'
})
export class OpcoesValoresNovoComponent implements OnInit {

  constructor() {
    initApp();
  }

  ngOnInit() { }

}
