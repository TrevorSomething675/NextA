import User from "../../../models/account/User"

export interface LoginRequest{
    password:string,
    email:string
};

export interface LoginResponse{
    user:User
}