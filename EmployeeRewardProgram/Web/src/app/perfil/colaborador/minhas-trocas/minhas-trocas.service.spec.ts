import { TestBed } from '@angular/core/testing';

import { MinhasTrocasService } from './minhas-trocas.service';

describe('MinhasTrocasService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: MinhasTrocasService = TestBed.get(MinhasTrocasService);
    expect(service).toBeTruthy();
  });
});
