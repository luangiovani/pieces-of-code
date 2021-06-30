import { TestBed } from '@angular/core/testing';

import { ConfigurarVerbasService } from './configurar-verbas.service';

describe('ConfigurarVerbasService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ConfigurarVerbasService = TestBed.get(ConfigurarVerbasService);
    expect(service).toBeTruthy();
  });
});
