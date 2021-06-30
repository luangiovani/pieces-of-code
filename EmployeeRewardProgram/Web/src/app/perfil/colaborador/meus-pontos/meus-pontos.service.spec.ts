import { TestBed } from '@angular/core/testing';

import { MeusPontosService } from './meus-pontos.service';

describe('MeusPontosService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: MeusPontosService = TestBed.get(MeusPontosService);
    expect(service).toBeTruthy();
  });
});
