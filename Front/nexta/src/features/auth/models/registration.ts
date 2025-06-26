import { AuthUser } from "../../../stores/AuthStore/models/AuthUser";

export interface RegistrationRequest {
    email:string,
    firstName:string,
    middleName:string,
    lastName:string,
    password:string,
    confirmPassword:string
}

export interface RegistrationResponse {
    user:AuthUser,
}