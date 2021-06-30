import { Component, OnInit } from '@angular/core';
declare function initApp(): any;

@Component({
  selector: 'app-trocar-produtos',
  templateUrl: './trocar-produtos.component.html'
})
export class TrocarProdutosComponent implements OnInit {

  constructor() {
    initApp();
   }

  ngOnInit() { }

}
