import { User } from "../../../shared/entities/User"

export interface IsAuthRequest{
    email:string
    role:string
}

export interface IsAuthResponse{
    user:User
    accessToken:string
}