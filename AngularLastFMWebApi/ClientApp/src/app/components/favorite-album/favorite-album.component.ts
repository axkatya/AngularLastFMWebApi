import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FavoriteAlbumService } from '../../services/favorite-album.service';
import { Album } from '../../models/album';

@Component({
    selector: 'app-favorite-album',
    templateUrl: './favorite-album.component.html',
    styleUrls: ['./favorite-album.component.css']
})
export class FavoriteAlbumComponent implements OnInit {

    albums: Album[];
    albumNameSearch: string;
    constructor(private favoriteAlbumService: FavoriteAlbumService,
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
                    this.favoriteAlbumService.searchFavoriteAlbumsByName(this.albumNameSearch).subscribe(result => {
                        this.albums = result;
                    });
                } else {
                    this.albumNameSearch = "";
                    this.favoriteAlbumService.searchFavoriteAlbums().subscribe(result => {
                        this.albums = result;
                    });
                }
            });
    }

    onClickSearchAlbum() {
        this.router.navigate(['favoriteAlbums'], { queryParams: { albumNameSearch: this.albumNameSearch } });
    }

    onClickSearchAlbumClear() {
        this.router.navigate(['favoriteAlbums']);
    }
}
