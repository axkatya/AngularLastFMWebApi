import { TestBed, inject } from '@angular/core/testing';
import { ArtistService } from './artist.service';
import { MockBackend } from '@angular/http/testing';
import { Http, BaseRequestOptions, RequestOptions } from '@angular/http';

let service: ArtistService;

describe('ArtistService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        ArtistService,
        MockBackend,
        BaseRequestOptions,
        {
          provide: Http,
          useFactory: (mockBackend: MockBackend, defaultOptions: RequestOptions) => {
            return new Http(mockBackend, defaultOptions);
          },
          deps: [MockBackend, BaseRequestOptions]
        }
      ]
    });
  });

  beforeEach(() => {
    service = TestBed.get(ArtistService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
