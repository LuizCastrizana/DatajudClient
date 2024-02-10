import { TestBed } from '@angular/core/testing';

import { RespostaApiService } from './resposta-api.service';

describe('RespostaApiService', () => {
  let service: RespostaApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RespostaApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
