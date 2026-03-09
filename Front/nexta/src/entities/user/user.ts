export interface User{
    firstName:string;
    middleName:string;
    lastName:string;
    email:string;
    phone:string;
    passwordhash:string;
    role:string;
    notifications?:Notification[]
}