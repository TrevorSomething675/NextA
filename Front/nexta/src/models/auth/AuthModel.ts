import User from "../account/User";

interface AuthModel{
    user?:User;
    accessToken?:string;
    refreshToken?:string;
    errors?:string;
}

export default AuthModel;