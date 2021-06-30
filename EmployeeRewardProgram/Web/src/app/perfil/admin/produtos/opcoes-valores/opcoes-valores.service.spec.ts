import { TestBed } from '@angular/core/testing';

import { OpcoesValoresService } from './opcoes-valores.service';

describe('OpcoesValoresService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: OpcoesValoresService = TestBed.get(OpcoesValoresService);
    expect(service).toBeTruthy();
  });
});
