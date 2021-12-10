import { SearchParamsBase } from './search-params-base';

export interface UserSearchParams extends SearchParamsBase {
    id?: number;
    roleId?: number;
    name?: string;
    email?: string;
    username?: string;
}