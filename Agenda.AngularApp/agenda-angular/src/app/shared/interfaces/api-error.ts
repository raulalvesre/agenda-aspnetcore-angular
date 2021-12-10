export interface ApiError {
    propertyName: string,
    errorMessage: string,
    attemptedValue: any,
    customState: any,
    errorCode: string[]
}
