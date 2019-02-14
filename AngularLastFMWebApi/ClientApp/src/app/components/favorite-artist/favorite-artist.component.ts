import { Component, OnInit } from '@angular/core';
import { Artist } from '../../models/artist';
import { FavoriteArtistService } from '../../services/favorite-artist.service';

@Component({
  selector: 'app-favorite-artist',
  templateUrl: './favorite-artist.component.html',
  styleUrls: ['./favorite-artist.component.css']
})
export class FavoriteArtistComponent implements OnInit {

  artists: Artist[];
  constructor(private favoriteArtistService: FavoriteArtistService) { }

  ngOnInit() {
    this.favoriteArtistService.searchFavoriteArtists().subscribe(result => {
      this.artists = result;
    });
  }

  onClickSearchArtist(artistNameSearch: string) {
    this.favoriteArtistService.searchFavoriteArtistsByName(artistNameSearch).subscribe(result => {
      this.artists = result;
    });
  }
}
