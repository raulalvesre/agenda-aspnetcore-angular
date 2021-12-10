import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { UserLoginRequest } from '../../shared/interfaces/user-login-req';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  constructor(private http: HttpClient) {}

  logIn(userLoginReq: UserLoginRequest): Observable<Object> {
    return this.http
      .post(environment.API + '/login', userLoginReq)
      .pipe(take(1));
  }
}
