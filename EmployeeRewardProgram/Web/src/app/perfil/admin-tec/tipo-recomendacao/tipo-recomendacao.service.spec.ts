import { TestBed } from '@angular/core/testing';

import { TipoRecomendacaoService } from './tipo-recomendacao.service';

describe('TipoRecomendacaoService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: TipoRecomendacaoService = TestBed.get(TipoRecomendacaoService);
    expect(service).toBeTruthy();
  });
});
