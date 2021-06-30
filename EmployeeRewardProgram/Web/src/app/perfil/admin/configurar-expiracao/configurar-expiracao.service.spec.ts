import { TestBed } from '@angular/core/testing';

import { ConfigurarExpiracaoService } from './configurar-expiracao.service';

describe('ConfigurarExpiracaoService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ConfigurarExpiracaoService = TestBed.get(ConfigurarExpiracaoService);
    expect(service).toBeTruthy();
  });
});
