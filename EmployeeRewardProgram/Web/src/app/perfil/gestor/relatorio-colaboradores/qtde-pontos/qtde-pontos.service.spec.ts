import { TestBed } from '@angular/core/testing';

import { QtdePontosService } from './qtde-pontos.service';

describe('QtdePontosService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: QtdePontosService = TestBed.get(QtdePontosService);
    expect(service).toBeTruthy();
  });
});
