import { TestBed } from '@angular/core/testing';

import { RelatoriosLojaService } from './relatorios-loja.service';

describe('RelatoriosLojaService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: RelatoriosLojaService = TestBed.get(RelatoriosLojaService);
    expect(service).toBeTruthy();
  });
});
