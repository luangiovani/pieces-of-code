import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RelatorioHistoricoAtribuicoesComponent } from './historico-atribuicoes.component';

describe('HistoricoAtribuicoesComponent', () => {
  let component: RelatorioHistoricoAtribuicoesComponent;
  let fixture: ComponentFixture<RelatorioHistoricoAtribuicoesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RelatorioHistoricoAtribuicoesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RelatorioHistoricoAtribuicoesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
