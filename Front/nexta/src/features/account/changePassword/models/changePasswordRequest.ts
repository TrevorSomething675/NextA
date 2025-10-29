export interface ChangePasswordRequest{
    userId:string;
    email:string;
    password:string;
    legacyPassword:string;
    confirmPassword:string;
}