export interface RegisterFirstStepRequest{
    email:string,
    firstName:string,
    middleName?:string,
    lastName:string,
    password:string,
    confirmPassword:string,
    code: string
}