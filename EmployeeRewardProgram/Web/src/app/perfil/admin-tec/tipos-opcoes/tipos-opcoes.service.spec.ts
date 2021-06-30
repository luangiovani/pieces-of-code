import { TestBed } from '@angular/core/testing';

import { TiposOpcoesService } from './tipos-opcoes.service';

describe('TiposOpcoesService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: TiposOpcoesService = TestBed.get(TiposOpcoesService);
    expect(service).toBeTruthy();
  });
});
