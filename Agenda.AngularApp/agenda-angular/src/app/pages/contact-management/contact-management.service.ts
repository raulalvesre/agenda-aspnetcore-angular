import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseContactService } from 'src/app/shared/services/base-contact.service';
import { ContactAdminSearchParams } from './../../shared/interfaces/search-params/contact-admin-search-params';

@Injectable({
  providedIn: 'root',
})
export class ContactManagementService extends BaseContactService {
  getResourceUrl(): string {
    return '/admin/contatos';
  }

  constructor(http: HttpClient) {
    super(http);
  }

  mapSearchParamsToHttpParams(contactSearchParams: ContactAdminSearchParams) {
    return new HttpParams().appendAll({
      skip: contactSearchParams.skip ?? '',
      take: contactSearchParams.take ?? '',
      idUsuario: contactSearchParams.idUsuario ?? '',
      idContato: contactSearchParams.idContato ?? '',
      nomeContato: contactSearchParams?.nomeContato ?? '',
      ddd: contactSearchParams?.ddd ?? '',
      numeroTelefone: contactSearchParams?.numeroTelefone ?? '',
    });
  }
}
