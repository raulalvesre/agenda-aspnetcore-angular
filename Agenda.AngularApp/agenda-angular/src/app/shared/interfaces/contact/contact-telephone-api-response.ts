import { Record } from '../record';

export interface ContactTelephoneApiResponse extends Record {
    type: string;
    description: string;
    ddd: number;
    telephoneOnlyNumbers: string;
    telephoneFormatted: string;
}