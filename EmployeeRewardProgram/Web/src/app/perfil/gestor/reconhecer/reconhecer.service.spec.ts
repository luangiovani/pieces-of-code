import { TestBed } from '@angular/core/testing';

import { ReconhecerService } from './reconhecer.service';

describe('ReconhecerService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ReconhecerService = TestBed.get(ReconhecerService);
    expect(service).toBeTruthy();
  });
});
