import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SolicitacoesFaturamentoComponent } from './solicitacoes-faturamento.component';

describe('SolicitacoesFaturamentoComponent', () => {
  let component: SolicitacoesFaturamentoComponent;
  let fixture: ComponentFixture<SolicitacoesFaturamentoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SolicitacoesFaturamentoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SolicitacoesFaturamentoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
