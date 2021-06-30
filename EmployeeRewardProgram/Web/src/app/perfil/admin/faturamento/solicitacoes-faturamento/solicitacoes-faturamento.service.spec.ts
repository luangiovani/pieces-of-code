import { TestBed } from '@angular/core/testing';

import { SolicitacoesFaturamentoService } from './solicitacoes-faturamento.service';

describe('SolicitacoesFaturamentoService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: SolicitacoesFaturamentoService = TestBed.get(SolicitacoesFaturamentoService);
    expect(service).toBeTruthy();
  });
});
