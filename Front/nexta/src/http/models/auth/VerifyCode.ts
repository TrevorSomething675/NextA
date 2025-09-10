import { AuthUser } from "../../../stores/AuthStore/models/AuthUser"

export interface VerifyCodeRequest{
    email:string,
    role:string,
    code:string
}

export interface VerifyCodeResponse {
    user: AuthUser,
    accessToken: string
}