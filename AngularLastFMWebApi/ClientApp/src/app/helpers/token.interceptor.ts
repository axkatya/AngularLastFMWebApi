﻿import { Injectable } from '@angular/core';
import {
	HttpRequest,
	HttpHandler,
	HttpEvent,
	HttpInterceptor
} from '@angular/common/http';
import { AuthenticationService } from '../services/authentication.service';

import { Observable } from 'rxjs/Observable';
@Injectable()
export class TokenInterceptor implements HttpInterceptor {
	constructor(public auth: AuthenticationService) { }
	intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

		var token = this.auth.getToken();
		if (token) {
			request = request.clone({
				setHeaders: {
					ContentType: 'application/json',
					Authorization: `Bearer ${this.auth.getToken()}`
				}
			});
		}

		return next.handle(request);
	}
}