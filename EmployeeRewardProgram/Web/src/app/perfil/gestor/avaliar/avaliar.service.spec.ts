import { TestBed } from '@angular/core/testing';

import { AvaliarService } from './avaliar.service';

describe('AvaliarService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: AvaliarService = TestBed.get(AvaliarService);
    expect(service).toBeTruthy();
  });
});
