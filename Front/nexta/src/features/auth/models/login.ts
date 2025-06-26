import { AuthUser } from "../../../stores/AuthStore/models/AuthUser"

export interface LoginRequest{
    password:string,
    email:string
};

export interface LoginResponse{
    user: AuthUser,
}