import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions } from '@angular/http';
import { Artist } from '../models/artist';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment';

@Injectable()
export class FavoriteArtistService {

  constructor(private http: Http) {
  }

  searchFavoriteArtists() {
    return this.http.get(environment.baseUrl + '/api/favoriteArtist/', this.jwt())
      .pipe(map(result => result.json() as Artist[]));
  }

  searchFavoriteArtistsByName(artistNameSearch: string) {
    return this.http.get(environment.baseUrl + '/api/favoriteArtist/' + artistNameSearch, this.jwt())
      .pipe(map(result => result.json() as Artist[]));
  }

  saveToFavoriteArtists(artist: Artist) {
    return this.http.post(environment.baseUrl + '/api/favoriteArtist', artist, this.jwt())
      .pipe(map(result => result.json() as number));
  }

  deleteFromFavoriteArtists(favoriteArtistId: number) {
    return this.http.delete(environment.baseUrl + '/api/favoriteArtist/' + favoriteArtistId, this.jwt()).pipe();
  }

  private jwt() {
    // create authorization header with jwt token
    const currentUser = JSON.parse(localStorage.getItem('currentUser'));
    if (currentUser && currentUser.token) {
      const headers = new Headers({ 'Authorization': 'Bearer ' + currentUser.token, 'Content-Type': 'application/json' });
      return new RequestOptions({ headers: headers });
    }
  }
}
