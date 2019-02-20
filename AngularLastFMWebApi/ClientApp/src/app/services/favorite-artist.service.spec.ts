import { TestBed, inject } from '@angular/core/testing';

import { FavoriteArtistService } from './favorite-artist.service';

describe('FavoriteArtistService', () => {
  beforeEach(() => {
	TestBed.configureTestingModule({
	  providers: [FavoriteArtistService]
	});
  });

  it('should be created', inject([FavoriteArtistService], (service: FavoriteArtistService) => {
	expect(service).toBeTruthy();
  }));
});
