import { Record } from '../record';
import { ContactTelephoneApiResponse } from './contact-telephone-api-response';

export interface ContactApiResponse extends Record {
    name: string;
    telephones: ContactTelephoneApiResponse[];
}