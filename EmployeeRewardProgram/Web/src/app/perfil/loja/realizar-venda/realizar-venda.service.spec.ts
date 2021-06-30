import { TestBed } from '@angular/core/testing';

import { RealizarVendaService } from './realizar-venda.service';

describe('RealizarVendaService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: RealizarVendaService = TestBed.get(RealizarVendaService);
    expect(service).toBeTruthy();
  });
});
