import { Component, OnInit, Input } from '@angular/core';
import { FavoriteAlbumService } from '../../services/favorite-album.service';
import { Album } from '../../models/album';

@Component({
  selector: 'app-album-item',
  templateUrl: './album-item.component.html',
  styleUrls: ['./album-item.component.css']
})
export class AlbumItemComponent implements OnInit {
  @Input() album: Album;
  largeImages: any[];
  largeImage: any;
  isFavorite: boolean;

  constructor(private favoriteAlbumService: FavoriteAlbumService) {

  }

  ngOnInit() {
    this.isFavorite = this.album.favoriteAlbumId > 0;
    if (this.album !== null && this.album !== undefined && this.album.image !== null && this.album.image !== undefined) {
      if (this.album.image.some((img: any) => img['size'] === 'large')) {
        this.largeImages = this.album.image
          .filter((img: any) => img['size'] === 'large');
        this.largeImage = this.largeImages[0];
      } else {
        this.largeImage = this.album.image[0];
      }
    }
  }

  onClickFavoriteButton() {
    if (this.album.favoriteAlbumId === 0) {
      this.addToFavoriteAlbum();
    } else {
      this.deleteFromFavoriteAlbum();
    }
  }

  addToFavoriteAlbum() {
    this.favoriteAlbumService.saveToFavoriteAlbums(this.album).subscribe(result => {
      this.album.favoriteAlbumId = result;
      this.isFavorite = this.album.favoriteAlbumId > 0;
    });
  }

  deleteFromFavoriteAlbum() {
    this.favoriteAlbumService.deleteFromFavoriteAlbums(this.album.favoriteAlbumId)
        .subscribe(() => {

                this.album.favoriteAlbumId = 0;
                this.isFavorite = false;
            }
        );
  }

  isString(val) { return typeof val === 'string'; }
}
