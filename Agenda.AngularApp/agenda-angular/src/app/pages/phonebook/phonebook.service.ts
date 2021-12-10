import { BaseContactService } from './../../shared/services/base-contact.service';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ContactSearchParams } from './../../shared/interfaces/search-params/contact-search-params';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PhonebookService extends BaseContactService{

  constructor(http: HttpClient) {
    super(http);
  }
  
  getResourceUrl(): string {
    return '/agenda';
  }

  mapSearchParamsToHttpParams(contactSearchParams: ContactSearchParams) {
    return new HttpParams().appendAll({
      skip: contactSearchParams.skip ?? '',
      take: contactSearchParams.take ?? '',
      idContato: contactSearchParams.idContato ?? '',
      nomeContato: contactSearchParams?.nomeContato ?? '',
      ddd: contactSearchParams?.ddd ?? '',
      numeroTelefone: contactSearchParams?.numeroTelefone ?? ''
    });
  }

}
