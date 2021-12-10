import { ContactApiResponse } from 'src/app/shared/interfaces/contact/contact-api-response';
import { UserApiResponse } from './../user/user-api-response';

export interface ContactApiAdminResponse extends ContactApiResponse {
    owner: UserApiResponse;
}