import { Component, OnInit } from '@angular/core';
declare function initApp(): any;
@Component({
  selector: 'app-opcoes',
  templateUrl: './opcoes.component.html'
})
export class OpcoesComponent implements OnInit {

  constructor() {
    initApp();
  }

  ngOnInit() { }

}
