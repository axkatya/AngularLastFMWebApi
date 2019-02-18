import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Artist } from '../models/artist';
import { environment } from '../../environments/environment';

@Injectable()
export class FavoriteArtistService {

    constructor(private http: HttpClient) {
    }

    searchFavoriteArtists() {
        return this.http.get(environment.baseUrl + '/api/favoriteArtist/', this.jwt());
    }

    searchFavoriteArtistsByName(artistNameSearch: string) {
        return this.http.get(environment.baseUrl + '/api/favoriteArtist/' + artistNameSearch, this.jwt());
    }

    saveToFavoriteArtists(artist: Artist) {
        return this.http.post(environment.baseUrl + '/api/favoriteArtist', artist, this.jwt());
    }

    deleteFromFavoriteArtists(favoriteArtistId: number) {
        return this.http.delete(environment.baseUrl + '/api/favoriteArtist/' + favoriteArtistId, this.jwt());
    }

    private jwt() {
        // create authorization header with jwt token
        var currentUser = JSON.parse(localStorage.getItem('currentUser'));
        if (currentUser && currentUser.token) {
            const headers = new HttpHeaders()
                .set('Content-Type', 'application/json')
                .set('Authorization', 'Bearer ' + currentUser.token);
            return {
                headers: headers
            };
        }
    }
}
