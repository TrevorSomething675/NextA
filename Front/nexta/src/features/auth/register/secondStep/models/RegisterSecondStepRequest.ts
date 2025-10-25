export interface RegisterSecondStepRequest {
    email:string;
    firstName:string;
    lastName:string;
    middleName?:string;
    password?:string;
    confirmPassword?:string;
    code:string;
}