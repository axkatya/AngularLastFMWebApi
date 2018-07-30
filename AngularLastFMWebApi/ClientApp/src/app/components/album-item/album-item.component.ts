import { Component, OnInit, Input } from '@angular/core';
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

  constructor() { }

  ngOnInit() {
    if (this.album != null && this.album != undefined) {
      this.largeImages = this.album.image
        .filter((img: any) => img['size'] === 'large');
      this.largeImage = this.largeImages[0];
    }
  }

  isString(val) { return typeof val === 'string'; }
}
