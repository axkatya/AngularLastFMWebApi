import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { environment } from '../../environments/environment';

@Injectable()
export class AlbumService {

    constructor(private http: HttpClient) {
    }

    searchAlbum(albumNameSearch: string) {
        return this.http.get(environment.baseUrl + '/api/album/' + albumNameSearch);
    }
}
