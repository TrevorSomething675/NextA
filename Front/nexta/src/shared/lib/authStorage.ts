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

export const SetAuthData = (userId:string, firstName:string, lastName:string, 
    middleName:string, email:string, phone:string, accessToken:string, role:string
) => {
    localStorage.setItem('userId', userId ?? '');
    localStorage.setItem('firstName', firstName ?? '');
    localStorage.setItem('lastName', lastName ?? '');
    localStorage.setItem('middleName', middleName ?? '');
    localStorage.setItem('email', email ?? '');
    localStorage.setItem('phone', phone ?? '');
    localStorage.setItem('accessToken', accessToken);
    localStorage.setItem('isAuth', 'true');
    localStorage.setItem('role', role ?? '');
}

export const SetToken = (accessToken:string) => {
    localStorage.setItem('accessToken', accessToken);
    localStorage.setItem('isAuth', 'true');
}