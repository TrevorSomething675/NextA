interface User{
    id:string;
    email:string | null;
    firstName:string | null;
    middleName:string | null;
    lastName:string | null;
    role:string  | null;
    passwordHash:string | null;
    phone:number
}

export default User;