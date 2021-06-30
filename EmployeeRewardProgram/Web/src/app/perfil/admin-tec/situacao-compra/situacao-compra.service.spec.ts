import { TestBed } from '@angular/core/testing';

import { SituacaoCompraService } from './situacao-compra.service';

describe('SituacaoCompraService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: SituacaoCompraService = TestBed.get(SituacaoCompraService);
    expect(service).toBeTruthy();
  });
});
