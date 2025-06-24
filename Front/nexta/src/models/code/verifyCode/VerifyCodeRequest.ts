interface VerifyCodeRequest{
    email?:string,
    userId?:string,
    role?:string,
    code?:string
}

export default VerifyCodeRequest;