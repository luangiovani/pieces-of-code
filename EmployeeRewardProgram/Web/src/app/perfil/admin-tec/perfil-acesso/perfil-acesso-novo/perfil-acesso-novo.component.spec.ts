import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PerfilAcessoNovoComponent } from './perfil-acesso-novo.component';

describe('PerfilAcessoNovoComponent', () => {
  let component: PerfilAcessoNovoComponent;
  let fixture: ComponentFixture<PerfilAcessoNovoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PerfilAcessoNovoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PerfilAcessoNovoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
