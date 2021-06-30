import { TestBed } from '@angular/core/testing';

import { HistoricoTrocasService } from './historico-trocas.service';

describe('HistoricoTrocasService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: HistoricoTrocasService = TestBed.get(HistoricoTrocasService);
    expect(service).toBeTruthy();
  });
});
