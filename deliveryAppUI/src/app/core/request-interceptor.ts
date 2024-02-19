import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpSentEvent,
  HttpHeaderResponse,
  HttpProgressEvent,
  HttpResponse,
  HttpUserEvent,
} from '@angular/common/http';

import { Router } from '@angular/router';

import { Observable } from 'rxjs';
import { TokenService } from '../services/token.service';

@Injectable()
export class RequestInterceptor implements HttpInterceptor {
  constructor(private _tokenService: TokenService, private _router: Router) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<
    | HttpSentEvent
    | HttpHeaderResponse
    | HttpProgressEvent
    | HttpResponse<any>
    | HttpUserEvent<any>
  > {
    if (this._tokenService.hasToken() && this._tokenService.isTokenExpires()) {
      this._tokenService.removeToken();
      this._router.navigate(['login']);
      // return;
    }

    if (this._tokenService.hasToken()) {
      const token = this._tokenService.getToken();

      if (req.headers.has('Content-Type')) {
        req = req.clone({
          setHeaders: {
            Accept: '*/*',
            'Content-Type':
              'multipart/form-data; boundary=<calculated when request is sent>',
            Authorization: `Bearer ${token}`,
          },
        });
      } else {
        req = req.clone({
          setHeaders: {
            'Content-Type': 'application/json',
            Authorization: `Bearer ${token}`,
          },
        });
      }
    }

    return next.handle(req);
  }
}
