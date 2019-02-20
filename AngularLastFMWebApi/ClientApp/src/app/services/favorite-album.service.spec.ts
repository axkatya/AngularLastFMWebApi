import { TestBed, inject } from '@angular/core/testing';

import { FavoriteAlbumService } from './favorite-album.service';

describe('FavoriteAlbumService', () => {
  beforeEach(() => {
	TestBed.configureTestingModule({
	  providers: [FavoriteAlbumService]
	});
  });

  it('should be created', inject([FavoriteAlbumService], (service: FavoriteAlbumService) => {
	expect(service).toBeTruthy();
  }));
});
