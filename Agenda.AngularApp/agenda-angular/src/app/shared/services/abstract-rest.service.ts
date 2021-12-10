import { SearchParamsBase } from './../interfaces/search-params/search-params-base';
import { Observable } from 'rxjs';
import { environment } from './../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { take } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export abstract class AbstractRestService<T> {
  protected readonly APIUrl = environment.API + this.getResourceUrl();

  constructor(protected http: HttpClient) { }

  abstract getResourceUrl(): string;

  create(record: T): Observable<any> {
    return this.http.post(`${this.APIUrl}`, record)
      .pipe(
        take(1)
      );
  }

  getPage(searchParams: SearchParamsBase): Observable<any> {
    let params = this.mapSearchParamsToHttpParams(searchParams);

    return this.http
      .get(
        `${this.APIUrl}/buscar`,
        { params }
      )
      .pipe(
        take(1)
      );
  }

  abstract mapSearchParamsToHttpParams(searchParams: SearchParamsBase): HttpParams;

  update(recordId: number, record: T): Observable<any> {
    return this.http.put(`${this.APIUrl}/${recordId}`, record)
      .pipe(
        take(1)
      );
  }

  delete(recordId: number): Observable<any> {
    return this.http.delete(`${this.APIUrl}/${recordId}`)
      .pipe(
        take(1)
      );
  }

}
