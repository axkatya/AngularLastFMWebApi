import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Artist } from '../models/artist';
import { environment } from '../../environments/environment';

@Injectable()
export class FavoriteArtistService {

    constructor(private http: HttpClient) {
    }

    searchFavoriteArtists() {
        return this.http.get(environment.baseUrl + '/api/favoriteArtist/');
    }

    searchFavoriteArtistsByName(artistNameSearch: string) {
        return this.http.get(environment.baseUrl + '/api/favoriteArtist/' + artistNameSearch);
    }

    saveToFavoriteArtists(artist: Artist) {
        return this.http.post(environment.baseUrl + '/api/favoriteArtist', artist);
    }

    deleteFromFavoriteArtists(favoriteArtistId: number) {
        return this.http.delete(environment.baseUrl + '/api/favoriteArtist/' + favoriteArtistId);
    }
}
