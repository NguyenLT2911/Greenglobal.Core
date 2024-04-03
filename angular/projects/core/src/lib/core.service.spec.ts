import { TestBed } from '@angular/core/testing';
import { CoreService } from './services/core.service';
import { RestService } from '@abp/ng.core';

describe('CoreService', () => {
  let service: CoreService;
  const mockRestService = jasmine.createSpyObj('RestService', ['request']);
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        {
          provide: RestService,
          useValue: mockRestService,
        },
      ],
    });
    service = TestBed.inject(CoreService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
