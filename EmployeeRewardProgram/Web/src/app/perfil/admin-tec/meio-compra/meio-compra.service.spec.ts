import { TestBed } from '@angular/core/testing';

import { MeioCompraService } from './meio-compra.service';

describe('MeioCompraService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: MeioCompraService = TestBed.get(MeioCompraService);
    expect(service).toBeTruthy();
  });
});
