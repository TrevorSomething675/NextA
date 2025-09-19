import { AuthUser } from "../../../stores/AuthStore/models/AuthUser"

export interface ConfirmUpdateEmailRequest{
    legacyEmail:string,
    email:string,
    code:string
}

export interface ConfirmUpdateEmailResponse{
    user:AuthUser
}