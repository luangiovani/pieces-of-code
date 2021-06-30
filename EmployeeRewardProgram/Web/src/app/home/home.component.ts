import { Component, OnInit } from '@angular/core';
declare function initApp(): any;
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit {
  title: 'Home';
  constructor() {
    this.title = 'Home';
    initApp();
  }

  ngOnInit() { }

}
