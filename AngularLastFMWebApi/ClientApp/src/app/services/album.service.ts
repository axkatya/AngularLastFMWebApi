import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { environment } from '../../environments/environment';

@Injectable()
export class AlbumService {

    constructor(private http: HttpClient) {
    }

    searchAlbum(albumNameSearch: string) {
        return this.http.get(environment.baseUrl + '/api/album/' + albumNameSearch, this.jwt());
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
