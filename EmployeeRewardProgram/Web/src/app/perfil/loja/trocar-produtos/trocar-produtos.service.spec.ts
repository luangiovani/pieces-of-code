import { TestBed } from '@angular/core/testing';

import { TrocarProdutosService } from './trocar-produtos.service';

describe('TrocarProdutosService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: TrocarProdutosService = TestBed.get(TrocarProdutosService);
    expect(service).toBeTruthy();
  });
});
