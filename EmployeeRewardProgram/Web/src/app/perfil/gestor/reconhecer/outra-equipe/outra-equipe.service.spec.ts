import { TestBed } from '@angular/core/testing';

import { OutraEquipeService } from './outra-equipe.service';

describe('OutraEquipeService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: OutraEquipeService = TestBed.get(OutraEquipeService);
    expect(service).toBeTruthy();
  });
});
