import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs/operators';
import { UserService } from './user.service';
import { ResponseModel } from '../models/responseModel';
import { LoginModel } from '../models/loginModel';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private URL_API: string = environment.apiUrl;

  constructor(private _http: HttpClient, private _userService: UserService) {}

  public login(loginModel: LoginModel) {
    return this._http
      .post<ResponseModel>(`${this.URL_API}/auth`, loginModel, {
        observe: 'response',
      })
      .pipe(
        tap((resp) => {
          const token = resp.body!['accessToken'];
          this._userService.setToken(token);
          this._userService.getUserAuthenticate();
        })
      );
  }
}
