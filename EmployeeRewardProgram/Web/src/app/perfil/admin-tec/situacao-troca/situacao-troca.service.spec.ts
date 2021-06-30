import { TestBed } from '@angular/core/testing';

import { SituacaoTrocaService } from './situacao-troca.service';

describe('SituacaoTrocaService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: SituacaoTrocaService = TestBed.get(SituacaoTrocaService);
    expect(service).toBeTruthy();
  });
});
