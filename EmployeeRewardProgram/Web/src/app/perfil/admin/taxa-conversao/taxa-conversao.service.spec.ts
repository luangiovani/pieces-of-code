import { TestBed } from '@angular/core/testing';

import { TaxaConversaoService } from './taxa-conversao.service';

describe('TaxaConversaoService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: TaxaConversaoService = TestBed.get(TaxaConversaoService);
    expect(service).toBeTruthy();
  });
});
