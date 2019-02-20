import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Artist } from '../../models/artist';
import { FavoriteArtistService } from '../../services/favorite-artist.service';

@Component({
	selector: 'app-favorite-artist',
	templateUrl: './favorite-artist.component.html',
	styleUrls: ['./favorite-artist.component.css']
})
export class FavoriteArtistComponent implements OnInit {

	artistNameSearch: string;
	artists: Artist[];
	constructor(private favoriteArtistService: FavoriteArtistService,
		private route: ActivatedRoute,
		private router: Router) { }

	ngOnInit() {
		this.refreshData();
	}

	onClickSearchArtist() {
		this.router.navigate(['favoriteArtists'], { queryParams: { artistNameSearch: this.artistNameSearch } });
	}

	onClickSearchArtistClear() {
		this.router.navigate(['favoriteArtists']);
	}

	refresh() {
		this.refreshData();
	}

	private refreshData() {
		this.route
			.queryParams
			.subscribe(params => {

				var artistNameSearchParam = params['artistNameSearch'];
				if (artistNameSearchParam !== null &&
					artistNameSearchParam != undefined &&
					artistNameSearchParam.length > 0) {
					this.artistNameSearch = artistNameSearchParam;
					this.favoriteArtistService.searchFavoriteArtistsByName(this.artistNameSearch).subscribe(result => {
						this.artists = result as Artist[];
					});
				} else {
					this.artistNameSearch = "";
					this.favoriteArtistService.searchFavoriteArtists().subscribe(result => {
						this.artists = result as Artist[];
					});
				}

			});
	}
}
