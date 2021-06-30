import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OpcoesEntregaComponent } from './opcoes-entrega.component';

describe('OpcoesEntregaComponent', () => {
  let component: OpcoesEntregaComponent;
  let fixture: ComponentFixture<OpcoesEntregaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OpcoesEntregaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OpcoesEntregaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
