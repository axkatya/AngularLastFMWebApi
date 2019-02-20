import { Component, OnInit } from '@angular/core';
import { AlbumService } from '../../services/album.service';
import { Album } from '../../models/album';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
	selector: 'app-album',
	templateUrl: './album.component.html',
	styleUrls: ['./album.component.css']
})
export class AlbumComponent implements OnInit {
	albumNameSearch: string;
	albums: Album[];

	constructor(private albumService: AlbumService,
		private route: ActivatedRoute,
		private router: Router) { }

	ngOnInit() {
		this.route
			.queryParams
			.subscribe(params => {

				var albumNameSearchParam = params['albumNameSearch'];
				if (albumNameSearchParam !== null &&
					albumNameSearchParam != undefined &&
					albumNameSearchParam.length > 0) {
					this.albumNameSearch = albumNameSearchParam;
					this.albumService.searchAlbum(this.albumNameSearch).subscribe(
						(result) => {
						this.albums = result as Album[];
					});
				} else {
					this.albumNameSearch = "";
					this.albums = null;
				}
			});
	}

	onClickSearchAlbum() {
		this.router.navigate(['albums'], { queryParams: { albumNameSearch: this.albumNameSearch } });
	}

	onClickSearchAlbumClear() {
		this.router.navigate(['albums']);
	}
}
