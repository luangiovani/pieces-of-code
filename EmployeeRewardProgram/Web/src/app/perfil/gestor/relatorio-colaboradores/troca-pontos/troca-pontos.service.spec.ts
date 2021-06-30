import { TestBed } from '@angular/core/testing';

import { TrocaPontosService } from './troca-pontos.service';

describe('TrocaPontosService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: TrocaPontosService = TestBed.get(TrocaPontosService);
    expect(service).toBeTruthy();
  });
});
