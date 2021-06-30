import { TestBed } from '@angular/core/testing';

import { OpcoesService } from './opcoes.service';

describe('OpcoesService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: OpcoesService = TestBed.get(OpcoesService);
    expect(service).toBeTruthy();
  });
});
