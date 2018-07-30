import { Component, OnInit } from '@angular/core';
import { AlbumService } from '../../services/album.service';
import { Album } from '../../models/album';

@Component({
  selector: 'app-album',
  templateUrl: './album.component.html',
  styleUrls: ['./album.component.css']
})
export class AlbumComponent implements OnInit {

  albums: Album[];
  constructor(private albumService: AlbumService) { }

  ngOnInit() {
    
  }

  onClickSearchAlbum(albumNameSearch: string) {
    this.albumService.searchAlbum(albumNameSearch).subscribe(result => {
      this.albums = result;
    });
  }
}
