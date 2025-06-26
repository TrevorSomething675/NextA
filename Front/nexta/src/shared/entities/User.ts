export interface User{
    id:string;
    email:string | null;
    firstName:string | null;
    lastName:string | null;
    middleName:string | null;
    role:string  | null;
    passwordHash?:string | null;
    phone?:number
}