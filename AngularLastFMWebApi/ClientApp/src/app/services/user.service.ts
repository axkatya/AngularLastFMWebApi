import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { environment } from '../../environments/environment';
import { User } from '../models/User';

@Injectable()
export class UserService {
  constructor(private http: Http) { }

  create(user: User) {
    return this.http.post(environment.baseUrl + '/api/account/register', user, this.jwt());
  }

  // private helper methods

  private jwt() {
    // create authorization header with jwt token
    let currentUser = JSON.parse(localStorage.getItem('currentUser'));
    if (currentUser && currentUser.token) {
      let headers = new Headers({ 'Authorization': 'Bearer ' + currentUser.token, 'Content-Type': 'application/json' });
      return new RequestOptions({ headers: headers });
    }
  }
}
