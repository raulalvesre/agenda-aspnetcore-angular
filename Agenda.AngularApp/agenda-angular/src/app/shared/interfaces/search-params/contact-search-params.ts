import { SearchParamsBase } from './search-params-base';

export interface ContactSearchParams extends SearchParamsBase {
    idContato: number;
    nomeContato: string;
    ddd: number;
    numeroTelefone: string;
}