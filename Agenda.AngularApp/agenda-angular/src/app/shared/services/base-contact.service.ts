import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { AbstractRestService } from 'src/app/shared/services/abstract-rest.service';

@Injectable({
  providedIn: 'root'
})
export abstract class BaseContactService extends AbstractRestService<any> {

  constructor(http: HttpClient) {
    super(http);
  }

  getTelephoneTypes() {
    return this.http
      .get(`${this.APIUrl}/tipos-telefone`)
      .pipe(
        take(1)
      );
  }

  isTelephoneNumberAlreadyRegistered(telephoneNumber: string, contactOwnerId: number = 0): Observable<boolean>{
    let params = new HttpParams().append('telefone', telephoneNumber);
    
    if (contactOwnerId != 0)
      params = params.append('usuarioId', contactOwnerId);

    return this.http
      .get<boolean>(`${this.APIUrl}/telefone-ja-registrado`, { params })
      .pipe(
        take(1)
      );
  }

}
