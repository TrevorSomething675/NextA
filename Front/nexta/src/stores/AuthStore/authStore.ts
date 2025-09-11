import { AuthUser } from "./models/AuthUser";
import { makeAutoObservable } from "mobx";

class AuthStore {
    user:AuthUser = {} as AuthUser
    isAuthenticated:boolean = false
    error:string = ''
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
        }
        catch(error) {
            this.user = user;
            this.isAuthenticated = false;

            if(error instanceof Error){
                this.error = error.message;
            } 
            else{
                this.error = 'Неизвестная ошибка после первого шага авторизации';
            }
        }
    }

    setUserData = (user:AuthUser) => {
        this.user = user;
        this.isAuthenticated = true;
        this.user.role = user.role
        this.setRole(user.role ?? 'User');
        this.isAdmin = user.role === 'Admin';
    }

    secondStepAuthenticate = async (user: AuthUser) => {
        try{
            this.user = user;
            this.isAuthenticated = true;
            this.user.role = user.role
            this.isAdmin = user.role === 'Admin';
            
            this.setRole(user.role ?? 'User');
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
}

export default new AuthStore();