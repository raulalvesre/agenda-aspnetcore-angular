import { Record } from '../record';

export interface UserApiResponse extends Record {
  role: string;
  username: string;
  name: string;
  email: string;
}
