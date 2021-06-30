import { TestBed } from '@angular/core/testing';

import { PerfilAcessoService } from './perfil-acesso.service';

describe('PerfilAcessoService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: PerfilAcessoService = TestBed.get(PerfilAcessoService);
    expect(service).toBeTruthy();
  });
});
