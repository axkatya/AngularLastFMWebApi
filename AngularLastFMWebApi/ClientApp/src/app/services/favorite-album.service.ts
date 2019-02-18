import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Album } from '../models/album';
import { environment } from '../../environments/environment';

@Injectable()
export class FavoriteAlbumService {

    constructor(private http: HttpClient) {
    }

    searchFavoriteAlbums() {
        return this.http.get(environment.baseUrl + '/api/favoriteAlbum/', this.jwt());
    }

    searchFavoriteAlbumsByName(albumNameSearch: string) {
        return this.http.get(environment.baseUrl + '/api/favoriteAlbum/' + albumNameSearch, this.jwt());
    }

    saveToFavoriteAlbums(album: Album) {
        return this.http.post(environment.baseUrl + '/api/favoriteAlbum', album, this.jwt());
    }

    deleteFromFavoriteAlbums(favoriteAlbumId: number) {
        return this.http.delete(environment.baseUrl + '/api/favoriteAlbum/' + favoriteAlbumId, this.jwt());
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
