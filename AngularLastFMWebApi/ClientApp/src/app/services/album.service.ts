import { Injectable, Inject } from '@angular/core';
import { map } from 'rxjs/operators';
import { Http, Headers, RequestOptions } from '@angular/http';
import { Album } from '../models/album';
import { environment } from '../../environments/environment';

@Injectable()
export class AlbumService {

  constructor(private http: Http) {
  }

  searchAlbum(albumNameSearch: string) {
    return this.http.get(environment.baseUrl + '/api/album/' + albumNameSearch, this.jwt())
      .pipe(map(result => result.json() as Album[]));
    //return this.http.get(environment.endPoint +'?method=album.search&album=' +
    //    albumNameSearch +
    //  '&api_key=' + environment.apiKey + '&format=json')
    //  .pipe(map(result => result.json().results.albummatches.album as Album[]));
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
