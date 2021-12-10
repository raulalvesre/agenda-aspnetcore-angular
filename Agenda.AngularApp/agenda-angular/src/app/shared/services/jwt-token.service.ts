import { ConnectedOverlayPositionChange } from '@angular/cdk/overlay';
import { Injectable } from '@angular/core';
import jwt_decode from 'jwt-decode';

import { TokenInformation } from '../interfaces/token-information';

@Injectable({
  providedIn: 'root'
})
export class JwtTokenService {

  jwtToken: string;
  decodedToken: { [key: string]: string };

  constructor() {
    this.setToken();
  }

  setToken(token: string = '') {
    if (!!token) {
      this.jwtToken = token;
      window.localStorage.setItem('jwt-token', token);
    } else {
      let localStorageToken = window.localStorage.getItem('jwt-token');
      if (localStorageToken != null)
        this.jwtToken = localStorageToken;
    }
  }

  destroyToken() {
    this.jwtToken = undefined;
    window.localStorage.removeItem('jwt-token');
  }

  decodeToken() {
    if (this.jwtToken != null)
      this.decodedToken = jwt_decode(this.jwtToken);
  }

  getUserObjectFromToken(): TokenInformation {
    this.decodeToken();

    if (this.decodedToken == null)
      return null;

    let user: TokenInformation = {
      id: +this.decodedToken.nameid,
      role: this.decodedToken.role,
      username: this.decodedToken.unique_name,
      email: this.decodedToken.email,
      exp: +this.decodedToken.exp
    };

    return user;
  }

  getDecodedToken() {
    return jwt_decode(this.jwtToken);
  }

  getExpiryTime() {
    this.decodeToken();
    return this.decodedToken ? this.decodedToken.exp : null;
  }

  isTokenExpired() {
    const expiryTime = this.getExpiryTime();
    if (expiryTime)
      return ((1000 * +expiryTime)) - (new Date().getTime()) < 500;

    return false;
  }

}
