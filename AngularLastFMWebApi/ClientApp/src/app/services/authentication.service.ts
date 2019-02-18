import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';
import { environment } from '../../environments/environment';


@Injectable()
export class AuthenticationService {
    constructor(private http: HttpClient) { }

    login(username: string, password: string) {
        const body = JSON.stringify({ username: username, password: password });
        const headerOptions = new HttpHeaders({ 'Content-Type': 'application/json' });

        return this.http.post<any>(environment.baseUrl + '/api/account/authenticate', body, { headers: headerOptions });
    }

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('currentUser');
    }
}
