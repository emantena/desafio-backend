import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';

import { environment } from '../../environments/environment';
import { TokenService } from './token.service';
import { StorageService } from './storage.service';

import { UserModel } from '../models/userModel';
import { ResponseModel } from '../models/responseModel';
import { UpdateUserModel } from '../models/updateUserModel';
import { ChangePasswordModel } from '../models/changePasswordModel';
import { BaseResponseModel } from '../models/response/baseResponseModel';

const URL_API: string = environment.apiUrl;

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private userSubject = new BehaviorSubject<UserModel>(new UserModel());
  private userName = '';
  private initials = '';
  private role = '';
  private userId = 0;

  constructor(
    private _tokenService: TokenService,
    private _storageService: StorageService,
    private _http: HttpClient
  ) {
    if (this._tokenService.hasToken()) {
      this.notifyUserLogged();
    }
  }

  setToken(token: string) {
    this._tokenService.setToken(token);
  }

  public getUser() {
    return this.userSubject.asObservable();
  }

  public logout() {
    this._tokenService.removeToken();
    this._storageService.removeLocalStorageItem('user');
    // this.userSubject.next(null);
  }

  public isLogged() {
    return (
      this._tokenService.hasToken() && !this._tokenService.isTokenExpires()
    );
  }

  public getUserName() {
    return this.userName;
  }

  public getRole() {
    return this.role;
  }

  public getInitialName() {
    return this.initials;
  }

  public getToken(): string {
    return this._tokenService.getToken() ?? '';
  }

  public getUserId(): number {
    return this.userId;
  }

  public getUserAuthenticate() {
    return this._http
      .get<BaseResponseModel>(`${URL_API}/user/profile`)
      .subscribe((resp: BaseResponseModel) => {
        this._storageService.setLocalStorageItem(
          'user',
          JSON.stringify(resp.data)
        );
        this.notifyUserLogged();
      });
  }

  public updateUser(updateUser: UpdateUserModel) {
    return this._http.patch<ResponseModel>(`${URL_API}/user`, updateUser);
  }

  public changePassword(changePassword: ChangePasswordModel) {
    return this._http.patch<ResponseModel>(
      `${URL_API}/user/change-password`,
      changePassword
    );
  }

  private notifyUserLogged() {
    let user = this.getUserFromStorage();

    if (!user || user.name === undefined) {
      this.getUserAuthenticate();

      user = this.getUserFromStorage();
    }

    if (user) {
      const arrayName = user.name.split(' ');

      this.userName = user.name;
      this.role = user.role;
      this.userId = parseInt(user.userId);

      this.initials = `${arrayName[0].substring(0, 1)}${arrayName[0].substring(
        1,
        1
      )}`;
      this.userSubject.next(user);
    }
  }

  private getUserFromStorage(): UserModel {
    const userJson = this._storageService.getLocalStorageItem('user');

    if (userJson === '') {
      return new UserModel();
    }

    const userResult = JSON.parse(userJson) as UserModel;

    return userResult;
  }
}
