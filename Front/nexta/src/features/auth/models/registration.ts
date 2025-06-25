import User from "../../../models/account/User"

export interface RegistrationRequest {
    email:string,
    firstName:string,
    middleName:string,
    lastName:string,
    password:string,
    confirmPassword:string,
    type: 'registration'
}

export interface RegistrationResponse {
    user:User,
    accessToken:string
}