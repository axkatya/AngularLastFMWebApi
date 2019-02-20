import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Album } from '../../models/album';

@Component({
	selector: 'app-album-list',
	templateUrl: './album-list.component.html',
	styleUrls: ['./album-list.component.css']
})
export class AlbumListComponent {
	@Input() albums: Album[];
	@Output() refreshEvent = new EventEmitter();

	refresh() {
		this.refreshEvent.emit();
	}
}
