import { AuthUser } from "./models/AuthUser";
import { makeAutoObservable } from "mobx";

class AuthStore {
    user:AuthUser = {} as AuthUser
    isAuthenticated:boolean = false
    error:string = ''
    readyToAuth:boolean = false
    isAdmin = false

    constructor(){
        makeAutoObservable(this);
        this.initialize();
    }
    
    logout = () => {
        this.isAuthenticated = false;
        this.user = {} as AuthUser;
        this.isAdmin = false;
    }

    setAdminStatus = (isAdmin:boolean) => {
        this.isAdmin = isAdmin;
    }
    firstStepAuthenticate = (user: AuthUser) => {
        try{
            this.user = user;
            this.isAuthenticated = false;
            this.readyToAuth = true;
        }
        catch(error) {
            this.user = user;
            this.isAuthenticated = false;
            this.readyToAuth = false;

            if(error instanceof Error){
                this.error = error.message;
            } 
            else{
                this.error = 'Неизвестная ошибка после первого шага авторизации';
            }
        }
    }

    secondStepAuthenticate = async (user: AuthUser) => {
        try{
            this.user = user;
            this.isAuthenticated = true;
            this.readyToAuth = false;
            this.user.role = user.role
            this.setRole(user.role ?? 'User');
            this.isAdmin = user.role === 'Admin';
        }
        catch(error){
            if(error instanceof Error){
                this.error = error.message;
            }
            else{
                this.error = 'Неизвестная ошибка после второго шага авторизации'
            }
        }
    }

    private setRole = (role:string) => {
        localStorage.setItem('role', role);
    }

    private initialize = () => {
        const user = {
            id: localStorage.getItem('userId'),
            email: localStorage.getItem('email'),
            firstName: localStorage.getItem('firstName'),
            lastName: localStorage.getItem('lastName'),
            middleName: localStorage.getItem('middleName'),
            phone: localStorage.getItem('phone'),
            accessToken: localStorage.getItem('accessToken'),
            role: localStorage.getItem('role')
        } as AuthUser

        this.isAuthenticated = localStorage.getItem('isAuth')?.toLowerCase() === "true" ? true : false
        this.isAdmin = localStorage.getItem('role') === 'Admin';
        this.user = user;
    }

    private setAuthData = (user:AuthUser) => {
        localStorage.setItem('userId', user.id ?? '');
        localStorage.setItem('email', user.email ?? '');
        localStorage.setItem('firstName', user.firstName ?? '');
        localStorage.setItem('middleName', user.middleName ?? '');
        localStorage.setItem('lastName', user.lastName ?? '');
        localStorage.setItem('phone', user.phone?.toString() ?? '');
        localStorage.setItem('accessToken', user.accessToken ?? '');
    }
}

export default new AuthStore();