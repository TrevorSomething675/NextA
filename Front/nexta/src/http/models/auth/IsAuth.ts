import { User } from "../../../sharedLegacy/entities/User"

export interface IsAuthRequest{
    email:string
    role:string
}

export interface IsAuthResponse{
    user:User
    accessToken:string
}