import { makeAutoObservable } from 'mobx';
import User from '../models/account/User';
import { AuthService } from '../features/auth/services/AuthService';
import { LoginRequest } from '../features/auth/models/login';
import { RegistrationRequest } from '../features/auth/models/registration';

class Auth{
    user = {} as User;
    isAuth = false;

    constructor(){
        makeAutoObservable(this);
        this.initializeFromStorage();
    }
    
    setAuth(isAuth:boolean){
        localStorage.setItem('isAuth', 'true');
        this.isAuth = isAuth;
    }
    
    async checkAuth(){
        const isAuth = Boolean(localStorage?.getItem('isAuth'));
        if(isAuth){
            try{
                const userId = localStorage?.getItem('id');
                const role = localStorage?.getItem('role');
                if(userId != null && role != null){
                    const response = await AuthService.isAuth({userId, role});
                    if(response.user){
                        this.setUserData(response.user);
                    }
                }
                this.initializeFromStorage();
            }
            catch {
                this.cleanUserData();
            }
        } else {
            this.cleanUserData();
        }
    }
    
    async login(data:LoginRequest)
    {
        try{
            const response = await AuthService.login(data);
            if(response.user){
                this.setUserData(response.user);
            }
            return response;
        }
        catch(error){
            throw error;
        }
    }
    
    async setUserDatainStorage(user:User){
        this.setUserData(user)
    }

    async register(data:RegistrationRequest){
        try{
            const response = await AuthService.register(data);
            if(response.user){
                this.setUserData(response.user);
            }
            return response;
        }
        catch(error){
            throw error;
        }
    }
    
    async logout(){
        try{
            this.cleanUserData();
        } catch(error) {
            throw error;
        }
    }
    
    private initializeFromStorage(): void {
        if(window === undefined) return;
        this.user = {
            id: localStorage.getItem('id') ?? '',
            email: localStorage.getItem('email') ?? '',
            firstName: localStorage.getItem('firstName') ?? '',
            lastName: localStorage.getItem('lastName') ?? '',
            middleName: localStorage.getItem('middleName') ?? '',
            role: localStorage.getItem('role') ?? '',
        }
        this.isAuth = localStorage.getItem('isAuth') === 'true';
    }

    private setUserData(user:User): void {
        if(window === undefined) return;

        Object.entries(user).forEach(([key, value]) => {
            if(value !== undefined) {
                localStorage.setItem(key, String(value));
            }
        });
        this.user = user;
    }

    private cleanUserData(): void {
        ['id', 'email', 'firstName', 'lastName', 'middleName', 'phone', 'isAuth', 'accessToken'].forEach((key) => {
            localStorage.removeItem(key);
        })
        this.user = {} as User;
        this.isAuth = false;
    }
}

export default new Auth();