export interface VerifyCodeRequest{
    email:string,
    userId:string,
    role:string,
    code:string
}

export interface VerifyCodeResponse{
    accessToken:string
}