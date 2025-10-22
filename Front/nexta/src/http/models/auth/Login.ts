import { User } from "../../../sharedLegacy/entities/User"

export interface LoginRequest{
    password:string,
    email:string
};

export interface LoginResponse{
    user:User
}