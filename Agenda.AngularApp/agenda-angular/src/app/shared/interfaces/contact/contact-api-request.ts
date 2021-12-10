import { ContactTelephoneApiRequest } from "./contact-telephone-request";

export interface ContactApiRequest {
    name: string;
    telephones: ContactTelephoneApiRequest[];
}