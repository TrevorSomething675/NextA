import { User } from "../../../sharedLegacy/entities/User"

export interface CheckAuthRequest{
    email:string
    role:string
}

export interface CheckAuthResponse{
    user:User
    accessToken:string
}