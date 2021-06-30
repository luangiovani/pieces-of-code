import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HistoricoRecomendacoesComponent } from './historico-recomendacoes.component';

describe('HistoricoRecomendacoesComponent', () => {
  let component: HistoricoRecomendacoesComponent;
  let fixture: ComponentFixture<HistoricoRecomendacoesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HistoricoRecomendacoesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HistoricoRecomendacoesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
