import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { Http } from '@angular/http';
import { Artist } from '../models/artist';
import { Album } from '../models/album';
import { Track } from '../models/track';
import { environment } from '../../environments/environment';


@Injectable()
export class ArtistService {
  constructor(private http: Http) {

  }

  searchArtist(artistNameSearch: string) {
    return this.http.get('https://localhost:44355/api/artist/' + artistNameSearch)
      .pipe(map(result => result.json() as Artist));
    //return this.http.get(environment.endPoint + '?method=artist.getinfo&artist=' +
    //    artistNameSearch +
    //    '&api_key=' + environment.apiKey + '&format=json')
    //  .pipe(map(result => result.json().artist as Artist));
  }

  searchArtistTopTracks(artistNameSearch: string) {
    return this.http.get('https://localhost:44355/api/toptrack/' + artistNameSearch)
      .pipe(map(result => result.json() as Track[]));
    //return this.http.get(environment.endPoint + '?method=artist.gettoptracks&artist=' +
    //    artistNameSearch +
    //    '&api_key=' + environment.apiKey + '&format=json')
    //  .pipe(map(result => result.json().toptracks.track as Track[]));
  }

  searchArtistTopAlbums(artistNameSearch: string) {
    return this.http.get('https://localhost:44355/api/topalbum/' + artistNameSearch)
      .pipe(map(result => result.json() as Album[]));
    //return this.http.get(environment.endPoint + '?method=artist.gettopalbums&artist=' +
    //    artistNameSearch +
    //    '&api_key=' + environment.apiKey + '&format=json')
    //  .pipe(map(result => result.json().topalbums.album as Album[]));
  }

}
