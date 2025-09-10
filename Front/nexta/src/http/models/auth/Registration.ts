import { User } from "../../../shared/entities/User"

export interface RegistrationRequest {
    email:string,
    firstName:string,
    middleName:string,
    lastName:string,
    password:string,
    confirmPassword:string
    code: string
}

export interface RegistrationResponse {
    user:User
    accessToken:string
}