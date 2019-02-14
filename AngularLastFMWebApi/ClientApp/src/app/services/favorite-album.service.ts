import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { Http, Headers, RequestOptions } from '@angular/http';
import { Album } from '../models/album';
import { environment } from '../../environments/environment';

@Injectable()
export class FavoriteAlbumService {

  constructor(private http: Http) {
  }

  searchFavoriteAlbums() {
    return this.http.get(environment.baseUrl + '/api/favoriteAlbum/', this.jwt())
      .pipe(map(result => result.json() as Album[]));
  }

  searchFavoriteAlbumsByName(albumNameSearch: string) {
    return this.http.get(environment.baseUrl + '/api/favoriteAlbum/' + albumNameSearch, this.jwt())
      .pipe(map(result => result.json() as Album[]));
  }

  saveToFavoriteAlbums(album: Album){
    return this.http.post(environment.baseUrl + '/api/favoriteAlbum', album, this.jwt())
      .pipe(map(result => result.json() as number));
  }

  deleteFromFavoriteAlbums(favoriteAlbumId: number) {
    return this.http.delete(environment.baseUrl + '/api/favoriteAlbum/' + favoriteAlbumId, this.jwt()).pipe();
  }

  private jwt() {
    // create authorization header with jwt token
    let currentUser = JSON.parse(localStorage.getItem('currentUser'));
    if (currentUser && currentUser.token) {
      let headers = new Headers({ 'Authorization': 'Bearer ' + currentUser.token, 'Content-Type': 'application/json' });
      return new RequestOptions({ headers: headers });
    }
  }
}
