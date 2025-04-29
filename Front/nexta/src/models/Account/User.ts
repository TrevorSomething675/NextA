interface User{
    id: string;
    email: string | null;
    firstName: string | null;
    middleName: string | null;
    lastName: string | null;
    role: string  | null;
    passwordHash: string | null;
}

export default User;