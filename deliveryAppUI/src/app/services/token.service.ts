import { Injectable } from '@angular/core';
import { jwtDecode } from "jwt-decode";


const KEY = 'authToken';

@Injectable({
  providedIn: 'root'
})

export class TokenService {
  public hasToken() {
    return !!this.getToken();
  }

  public setToken(token: string) {
    window.localStorage.setItem(KEY, token);
  }

  public getToken() {
    return window.localStorage.getItem(KEY);
  }

  public removeToken() {
    window.localStorage.removeItem(KEY);
  }

  public isTokenExpires(): boolean {
    const strtoken = this.getToken();

    if(strtoken == null){
      return true;
    }

    const token = jwtDecode(strtoken);

    return token.exp! < (Date.now() / 1000);
  }
}
