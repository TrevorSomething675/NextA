export const ClearAuthStore = () => {
    localStorage.removeItem('userId');
    localStorage.removeItem('email');
    localStorage.removeItem('firstName');
    localStorage.removeItem('middleName');
    localStorage.removeItem('lastName');
    localStorage.removeItem('phone');
    localStorage.removeItem('accessToken');
    localStorage.removeItem('isAuth');
    localStorage.removeItem('role');
}

export const SetAuthData = (userId:string | null, firstName:string, lastName:string, 
    middleName:string | null, email:string, phone:string | null | undefined, 
    accessToken:string, role:string
) => {
    localStorage.setItem('userId', userId ?? '');
    localStorage.setItem('firstName', firstName ?? '');
    localStorage.setItem('lastName', lastName ?? '');
    localStorage.setItem('middleName', middleName ?? '');
    localStorage.setItem('email', email ?? '');
    localStorage.setItem('phone', phone ?? '');
    localStorage.setItem('accessToken', accessToken);
    localStorage.setItem('role', role ?? '');

    localStorage.setItem('isAuth', 'true');
}

export const SetToken = (accessToken:string) => {
    localStorage.setItem('accessToken', accessToken);
    localStorage.setItem('isAuth', 'true');
}

export const ChangeAuthStatus = (status:boolean) => {
    localStorage.setItem('isAuth', status.toString());
}