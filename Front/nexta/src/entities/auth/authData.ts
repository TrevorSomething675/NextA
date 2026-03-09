export interface AuthData{
    id?:string;
    email:string;
    firstName:string;
    lastName:string;
    middleName?:string;
    role:string;
    phone?:string;
    password?:string;
    confirmPassword?:string;
    accessToken?:string
}