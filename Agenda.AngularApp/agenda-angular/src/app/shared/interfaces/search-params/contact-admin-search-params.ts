import { SearchParamsBase } from './search-params-base';

export interface ContactAdminSearchParams extends SearchParamsBase {
    idUsuario: number;
    idContato: number;
    nomeContato: string;
    ddd: number;
    numeroTelefone: string;
}