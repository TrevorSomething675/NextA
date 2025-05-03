import { makeAutoObservable } from 'mobx';
import User from '../models/account/User';
import LoginForm from '../models/auth/Login';
import AuthService from '../services/AuthService';
import RegisterForm from '../models/auth/Register';

class Auth{
    user = {
        id: localStorage?.getItem('id'),
        email: localStorage?.getItem('firstName'),
        lastName: localStorage?.getItem('lastName'),
        middleName: localStorage?.getItem('MiddleName'),
    } as User;
    isAuth = false;

    constructor(){
        makeAutoObservable(this);
        if(typeof window !== 'undefined')
        {
            this.user.email = localStorage?.getItem('email');
            this.user.lastName = localStorage?.getItem('lastName');
            this.user.firstName = localStorage?.getItem('firstName');
            this.user.middleName = localStorage?.getItem('middleName');
        }
    }
    setAuth(isAuth:boolean){
        this.isAuth = isAuth;
    }
    setUser(user:User){
        this.user = user;
    }

    async checkAuth(){
        const isAuth = Boolean(localStorage?.getItem('isAuth'));
        if(isAuth){
            this.setAuth(true);
        } else{
            this.setAuth(false);
        }
    }

    async login(data:LoginForm)
    {
        const response = await AuthService.login(data);
        if(response.statusCode != 200){
            return response;
        }
        localStorage.setItem('accessToken', response.value.accessToken!);
        localStorage.setItem('firstName', response.value.user.firstName!);
        localStorage.setItem('lastName', response.value.user.lastName!);
        localStorage.setItem('middleName', response.value.user.middleName!);
        localStorage.setItem('email', response.value.user.email!);
        localStorage.setItem('id', response.value.user.id!)
        localStorage.setItem('isAuth', 'true');
        this.setAuth(true);
        this.setUser(response.value.user);
        return response;
    }

    async register(registerData:RegisterForm){
        const response = await AuthService.register(registerData);
        localStorage.setItem('accessToken', response.value.accessToken!);
        localStorage.setItem('firstName', response.value.user.firstName!);
        localStorage.setItem('lastName', response.value.user.lastName!);
        localStorage.setItem('middleName', response.value.user.middleName!);
        localStorage.setItem('email', response.value.user.email!);
        localStorage.setItem('id', response.value.user.id!)
        localStorage.setItem('isAuth', 'true');
        this.setAuth(true);
        this.setUser(response.value.user);
        return response;
    }

    async logout(){
        try{
            this.setAuth(false);
            this.setUser({} as User);
            localStorage.removeItem('accessToken');
            localStorage.removeItem('firstName');
            localStorage.removeItem('lastName');
            localStorage.removeItem('middleName');
            localStorage.removeItem('email');
            localStorage.removeItem('id');
            localStorage.removeItem('isAuth');
        } catch(error) {
            console.error(error);
        }
    }
}

export default new Auth();