import { TestBed } from '@angular/core/testing';

import { VerbaService } from './verba.service';

describe('VerbaService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: VerbaService = TestBed.get(VerbaService);
    expect(service).toBeTruthy();
  });
});
