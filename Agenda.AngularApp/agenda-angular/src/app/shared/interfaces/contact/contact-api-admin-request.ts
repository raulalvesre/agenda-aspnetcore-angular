import { ContactTelephoneApiRequest } from "./contact-telephone-request";

export interface ContactApiAdminRequest {
    name: string;
    telephones: ContactTelephoneApiRequest[];
    ownerId?: number;
}