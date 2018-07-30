import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { Http } from '@angular/http';
import { Album } from '../models/album';
import { environment } from '../../environments/environment';

@Injectable()
export class AlbumService {

  constructor(private http: Http) {

  }

  searchAlbum(albumNameSearch: string) {
    return this.http.get('https://localhost:44355/api/album/' + albumNameSearch)
      .pipe(map(result => result.json() as Album[]));
    //return this.http.get(environment.endPoint +'?method=album.search&album=' +
    //    albumNameSearch +
    //  '&api_key=' + environment.apiKey + '&format=json')
    //  .pipe(map(result => result.json().results.albummatches.album as Album[]));
  } 
}
