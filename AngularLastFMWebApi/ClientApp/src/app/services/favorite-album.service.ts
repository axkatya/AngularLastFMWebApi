import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Album } from '../models/album';
import { environment } from '../../environments/environment';

@Injectable()
export class FavoriteAlbumService {

    constructor(private http: HttpClient) {
    }

    searchFavoriteAlbums() {
        return this.http.get(environment.baseUrl + '/api/favoriteAlbum/');
    }

    searchFavoriteAlbumsByName(albumNameSearch: string) {
        return this.http.get(environment.baseUrl + '/api/favoriteAlbum/' + albumNameSearch);
    }

    saveToFavoriteAlbums(album: Album) {
        return this.http.post(environment.baseUrl + '/api/favoriteAlbum', album);
    }

    deleteFromFavoriteAlbums(favoriteAlbumId: number) {
        return this.http.delete(environment.baseUrl + '/api/favoriteAlbum/' + favoriteAlbumId);
    }
}
