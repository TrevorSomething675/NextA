export interface AccessRecoveryFirstStepRequest{
    email:string;
}

export interface AccessRecoverySecondStepRequest{
    email:string;
    code:string;
    password:string;
    confirmPassword:string;
}