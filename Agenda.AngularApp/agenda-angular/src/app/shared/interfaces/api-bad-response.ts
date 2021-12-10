import { ApiError } from "./api-error";

export interface ApiBadResponse {
    message?: string,
    errors?: ApiError[],
    hasError: boolean
}