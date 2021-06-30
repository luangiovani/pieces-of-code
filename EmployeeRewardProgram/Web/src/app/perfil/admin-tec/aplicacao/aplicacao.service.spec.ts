import { TestBed } from '@angular/core/testing';

import { AplicacaoService } from './aplicacao.service';

describe('AplicacaoService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: AplicacaoService = TestBed.get(AplicacaoService);
    expect(service).toBeTruthy();
  });
});
