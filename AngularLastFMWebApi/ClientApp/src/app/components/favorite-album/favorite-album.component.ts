import { Component, OnInit } from '@angular/core';
import { FavoriteAlbumService } from '../../services/favorite-album.service';
import { Album } from '../../models/album';

@Component({
  selector: 'app-favorite-album',
  templateUrl: './favorite-album.component.html',
  styleUrls: ['./favorite-album.component.css']
})
export class FavoriteAlbumComponent implements OnInit {

  albums: Album[];
  constructor(private favoriteAlbumService: FavoriteAlbumService) { }

  ngOnInit() {
    this.favoriteAlbumService.searchFavoriteAlbums().subscribe(result => {
      this.albums = result;
    });
  }

  onClickSearchAlbum(albumNameSearch: string) {
    this.favoriteAlbumService.searchFavoriteAlbumsByName(albumNameSearch).subscribe(result => {
      this.albums = result;
    });
  }
}
