import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { environment } from '../../environments/environment';


@Injectable()
export class ArtistService {
    constructor(private http: HttpClient) {

    }

    searchArtist(artistNameSearch: string) {
        return this.http.get(environment.baseUrl + '/api/artist/' + artistNameSearch, this.jwt());
    }

    searchArtistTopTracks(artistNameSearch: string) {
        return this.http.get(environment.baseUrl + '/api/toptrack/' + artistNameSearch, this.jwt());
    }

    searchArtistTopAlbums(artistNameSearch: string) {
        return this.http.get(environment.baseUrl + '/api/topalbum/' + artistNameSearch, this.jwt());
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
