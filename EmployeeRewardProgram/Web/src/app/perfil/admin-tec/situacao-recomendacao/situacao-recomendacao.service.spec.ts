import { TestBed } from '@angular/core/testing';

import { SituacaoRecomendacaoService } from './situacao-recomendacao.service';

describe('SituacaoRecomendacaoService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: SituacaoRecomendacaoService = TestBed.get(SituacaoRecomendacaoService);
    expect(service).toBeTruthy();
  });
});
