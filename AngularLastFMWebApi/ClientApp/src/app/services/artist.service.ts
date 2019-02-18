import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { environment } from '../../environments/environment';


@Injectable()
export class ArtistService {
    constructor(private http: HttpClient) {

    }

    searchArtist(artistNameSearch: string) {
        return this.http.get(environment.baseUrl + '/api/artist/' + artistNameSearch);
    }

    searchArtistTopTracks(artistNameSearch: string) {
        return this.http.get(environment.baseUrl + '/api/toptrack/' + artistNameSearch);
    }

    searchArtistTopAlbums(artistNameSearch: string) {
        return this.http.get(environment.baseUrl + '/api/topalbum/' + artistNameSearch);
    }
}
