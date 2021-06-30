import { TestBed } from '@angular/core/testing';

import { HistoricoRecomendacoesService } from './historico-recomendacoes.service';

describe('HistoricoRecomendacoesService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: HistoricoRecomendacoesService = TestBed.get(HistoricoRecomendacoesService);
    expect(service).toBeTruthy();
  });
});
