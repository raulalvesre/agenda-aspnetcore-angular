import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { take } from 'rxjs/operators';
import { UserApiRequest } from 'src/app/shared/interfaces/user/user-api-request';
import { AbstractRestService } from 'src/app/shared/services/abstract-rest.service';
import { UserSearchParams } from './../../shared/interfaces/search-params/user-search-params';

@Injectable({
  providedIn: 'root',
})
export class UserManagementService extends AbstractRestService<UserApiRequest> {
  constructor(http: HttpClient) {
    super(http);
  }

  getResourceUrl(): string {
    return '/admin/usuarios';
  }

  mapSearchParamsToHttpParams(userSearchParams: UserSearchParams) {
    return new HttpParams().appendAll({
      skip: userSearchParams.skip ?? '',
      take: userSearchParams.take ?? '',
      id: userSearchParams.id ?? '',
      roleId: userSearchParams?.roleId ?? '',
      name: userSearchParams?.name ?? '',
      email: userSearchParams?.email ?? '',
      username: userSearchParams?.username ?? '',
    });
  }

  getUserRoles() {
    return this.http.get(`${this.APIUrl}/tipos`).pipe(take(1));
  }

  isUsernameAlreadyRegistered(username: string) {
    let params = new HttpParams().append('username', username);

    return this.http
      .get<boolean>(`${this.APIUrl}/username-ja-registrado`, { params })
      .pipe(take(1));
  }

  isEmailAlreadyRegistered(email: string) {
    let params = new HttpParams().append('email', email);

    return this.http
      .get<boolean>(`${this.APIUrl}/email-ja-registrado`, { params })
      .pipe(take(1));
  }
}
