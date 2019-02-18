import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { environment } from '../../environments/environment';
import { User } from '../models/User';

@Injectable()
export class UserService {
    constructor(private http: HttpClient) { }

  create(user: User) {
    return this.http.post(environment.baseUrl + '/api/account/register', user, this.jwt());
  }

  // private helper methods

  //private jwt() {
  //  // create authorization header with jwt token
  //  const currentUser = JSON.parse(localStorage.getItem('currentUser'));
  //  if (currentUser && currentUser.token) {
  //    const headers = new Headers({ 'Authorization': 'Bearer ' + currentUser.token, 'Content-Type': 'application/json' });
  //    return new RequestOptions({ headers: headers });
  //  }
  //}

    private jwt() {
        // create authorization header with jwt token
        const currentUser = JSON.parse(localStorage.getItem('currentUser'));
        if (currentUser && currentUser.token) {
            const headers = new HttpHeaders({ 'Authorization': 'Bearer ' + currentUser.token, 'Content-Type': 'application/json' });
            return {
                headers: headers
            };
        }
    }
}
