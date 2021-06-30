import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UsuarioLojaComponent } from './usuario-loja.component';

describe('UsuarioLojaComponent', () => {
  let component: UsuarioLojaComponent;
  let fixture: ComponentFixture<UsuarioLojaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UsuarioLojaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UsuarioLojaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
