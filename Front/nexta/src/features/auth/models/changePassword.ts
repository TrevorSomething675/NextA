export interface ChangePasswordRequest{
    userId:string
    email:string
    legacyPassword:string
    password:string
    confirmPassword:string
}