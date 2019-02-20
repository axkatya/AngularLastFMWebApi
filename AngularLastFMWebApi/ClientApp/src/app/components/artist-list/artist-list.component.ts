import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Artist } from '../../models/artist';

@Component({
	selector: 'app-artist-list',
	templateUrl: './artist-list.component.html',
	styleUrls: ['./artist-list.component.css']
})
export class ArtistListComponent {
	@Input() artists: Artist[];
	@Output() refreshEvent = new EventEmitter();

	refresh() {
		this.refreshEvent.emit();
	}
}
