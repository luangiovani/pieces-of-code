import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PerfilAcessoComponent } from './perfil-acesso.component';

describe('PerfilAcessoComponent', () => {
  let component: PerfilAcessoComponent;
  let fixture: ComponentFixture<PerfilAcessoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PerfilAcessoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PerfilAcessoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
