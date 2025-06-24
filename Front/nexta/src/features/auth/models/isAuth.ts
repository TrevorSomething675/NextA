import User from "../../../models/account/User"

export interface IsAuthRequest{
    userId:string,
    role:string
}

export interface IsAuthResponse{
    user:User,
    accessToken:string
}