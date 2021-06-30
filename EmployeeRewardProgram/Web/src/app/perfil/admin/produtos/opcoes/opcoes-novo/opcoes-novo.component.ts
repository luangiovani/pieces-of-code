import { Component, OnInit } from '@angular/core';
declare function initApp(): any;
@Component({
  selector: 'app-opcoes-novo',
  templateUrl: './opcoes-novo.component.html'
})
export class OpcoesNovoComponent implements OnInit {

  constructor() {
    initApp();
  }

  ngOnInit() { }

}
