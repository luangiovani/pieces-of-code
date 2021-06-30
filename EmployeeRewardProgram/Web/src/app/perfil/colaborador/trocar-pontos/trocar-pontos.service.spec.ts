import { TestBed } from '@angular/core/testing';

import { TrocarPontosService } from './trocar-pontos.service';

describe('TrocarPontosService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: TrocarPontosService = TestBed.get(TrocarPontosService);
    expect(service).toBeTruthy();
  });
});
