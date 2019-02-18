import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ArtistService } from '../../services/artist.service';
import { Artist } from '../../models/artist';
import { Track } from '../../models/track';
import { Album } from '../../models/album';

@Component({
    selector: 'app-artist',
    templateUrl: './artist.component.html',
    styleUrls: ['./artist.component.css']
})
export class ArtistComponent implements OnInit {

    artistNameSearch: string;
    artist: Artist;
    topTracks: Track[];
    topAlbums: Album[];

    constructor(private artistService: ArtistService,
        private route: ActivatedRoute,
        private router: Router) {

    }

    ngOnInit() {
        this.route
            .queryParams
            .subscribe(params => {

                var artistNameSearchParam = params['artistNameSearch'];
                if (artistNameSearchParam !== null &&
                    artistNameSearchParam != undefined &&
                    artistNameSearchParam.length > 0) {
                    this.artistNameSearch = artistNameSearchParam;
                } else {
                    this.artistNameSearch = "";
                }
                this.getAllInfo(this.artistNameSearch);
            });
    }

    onClickSearchArtist() {
        this.router.navigate(['artists'], { queryParams: { artistNameSearch: this.artistNameSearch } });
    }

    onClickSearchArtistClear() {
        this.router.navigate(['artists']);
    }

    getAllInfo(artistName: string) {
        if (artistName !== null &&
            artistName != undefined &&
            artistName.length > 0) {
            this.artistService.searchArtist(artistName).subscribe(result => {
                this.artist = result;
            });

            if (this.artist !== null && this.artist !== undefined) {
                this.artistService.searchArtistTopTracks(artistName).subscribe(result => {
                    this.topTracks = result;
                });

                this.artistService.searchArtistTopAlbums(artistName).subscribe(result => {
                    this.topAlbums = result;
                    if (this.topAlbums !== null) {
                        this.topAlbums.forEach(album => album.artist = artistName);
                    }
                });
            }
        } else {
            this.artist = null;
        }
    }
}
