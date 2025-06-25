import User from "../../../models/account/User"

export interface LoginRequest{
    password:string,
    email:string,
    type: 'login'
};

export interface LoginResponse{
    user:User,
    accessToken:string
}