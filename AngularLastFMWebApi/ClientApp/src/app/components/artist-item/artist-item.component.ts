import { Component, OnInit, OnChanges, Input, Output, EventEmitter } from '@angular/core';
import { Artist } from '../../models/artist';
import { Album } from '../../models/album';
import { Track } from '../../models/track';
import { FavoriteArtistService } from '../../services/favorite-artist.service';

@Component({
  selector: 'app-artist-item',
  templateUrl: './artist-item.component.html',
  styleUrls: ['./artist-item.component.css']
})
export class ArtistItemComponent implements OnInit, OnChanges {
  @Input('artist') artist: Artist;
  @Input('topTracks') topTracks: Track[];
	@Input('topAlbums') topAlbums: Album[];
	@Output() refreshEvent = new EventEmitter();
  largeImages: any[];
  largeImage: any;
  isFavorite: boolean;

  constructor(private favoriteArtistService: FavoriteArtistService) {

  }

  ngOnInit() {
	if (this.artist !== null && this.artist !== undefined) {
	  this.isFavorite = this.artist.favoriteArtistId > 0;
	}
  }

  ngOnChanges() {
	if (this.artist !== null && this.artist !== undefined) {
	  this.largeImages = this.artist.image
		.filter((img: any) => img['size'] === 'large');
	  this.largeImage = this.largeImages[0];
	}
  }

  onClickFavoriteButton() {
	if (this.artist.favoriteArtistId === 0) {
	  this.addToFavoriteAlbum();
	} else {
	  this.deleteFromFavoriteAlbum();
	  }

	  this.refreshEvent.emit();
  }

  addToFavoriteAlbum() {
	this.favoriteArtistService.saveToFavoriteArtists(this.artist).subscribe(result => {
	  this.artist.favoriteArtistId = +result;
	  this.isFavorite = this.artist.favoriteArtistId > 0;
	});
  }

  deleteFromFavoriteAlbum() {
	this.favoriteArtistService.deleteFromFavoriteArtists(this.artist.favoriteArtistId)
	  .subscribe(
		() => {
		  this.artist.favoriteArtistId = 0;
		  this.isFavorite = false;
		}
	  );
  }
}
