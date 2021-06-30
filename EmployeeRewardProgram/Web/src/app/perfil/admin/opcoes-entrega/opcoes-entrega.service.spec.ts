import { TestBed } from '@angular/core/testing';

import { OpcoesEntregaService } from './opcoes-entrega.service';

describe('OpcoesEntregaService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: OpcoesEntregaService = TestBed.get(OpcoesEntregaService);
    expect(service).toBeTruthy();
  });
});
