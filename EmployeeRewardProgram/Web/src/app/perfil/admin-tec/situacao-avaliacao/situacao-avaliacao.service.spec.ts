import { TestBed } from '@angular/core/testing';

import { SituacaoAvaliacaoService } from './situacao-avaliacao.service';

describe('SituacaoAvaliacaoService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: SituacaoAvaliacaoService = TestBed.get(SituacaoAvaliacaoService);
    expect(service).toBeTruthy();
  });
});
