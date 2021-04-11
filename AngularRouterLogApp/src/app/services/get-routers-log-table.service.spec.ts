import { TestBed } from '@angular/core/testing';

import { GetRoutersLogTableService } from './get-routers-log-table.service';

describe('GetRoutersLogTableService', () => {
  let service: GetRoutersLogTableService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GetRoutersLogTableService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
