import { Component, OnInit, Input } from '@angular/core';
import { Artist } from '../../models/artist';

@Component({
  selector: 'app-artist-list',
  templateUrl: './artist-list.component.html',
  styleUrls: ['./artist-list.component.css']
})
export class ArtistListComponent implements OnInit {
  @Input() artists: Artist[];
  constructor() { }

  ngOnInit() {
  }

}
