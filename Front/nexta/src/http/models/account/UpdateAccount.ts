import { User } from "../../../shared/entities/User"

export interface UpdateAccountRequest{
    id:string,
    firstName:string,
    middleName:string,
    lastName:string,
    email:string,
    phone:string
}

export interface UpdateAccountResponse{
    user:User
}