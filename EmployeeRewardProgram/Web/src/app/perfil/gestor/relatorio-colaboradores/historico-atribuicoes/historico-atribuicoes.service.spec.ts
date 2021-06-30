import { TestBed } from '@angular/core/testing';

import { HistoricoAtribuicoesService } from './historico-atribuicoes.service';

describe('HistoricoAtribuicoesService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: HistoricoAtribuicoesService = TestBed.get(HistoricoAtribuicoesService);
    expect(service).toBeTruthy();
  });
});
