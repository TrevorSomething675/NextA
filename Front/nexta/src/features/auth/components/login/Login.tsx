import { useState } from 'react';
import styles from './Login.module.css';
import { SubmitHandler, useForm } from 'react-hook-form';
import { LoginRequest } from '../../models/login';
import { GetBasketDetailsFilter, GetBasketDetailsRequest } from '../../../basket/models/GetBasketDetails';
import auth from '../../../../stores/auth';
import { BasketService } from '../../../basket/services/BasketService';
import basket from '../../../../stores/basket';
import { GetOrdersForUserFilter, GetOrdersForUserRequest } from '../../../order/models/GetOrdersForUserFilter';
import OrderService from '../../../../services/OrderService';
import orderStore from '../../../../stores/orderStore';
import { AuthService } from '../../services/AuthService';
import ErrorResponseModel from '../../../../models/ErrorResponseModel';

export const Login:React.FC<{changeCodeVerifyStatus:any, changeAuthStatus:any}> = ({changeCodeVerifyStatus, changeAuthStatus}) => {
    const { register, handleSubmit, formState: {errors} } = useForm<LoginRequest>();
    const [hasError, setError] = useState('');
    const [isLoading, setLoading] = useState(false);

    const handlerChangeFormStatus = () => {
        changeAuthStatus();
    }

    const fetchData = async() => {
        const filter:GetBasketDetailsFilter = {
            pageNumber: 1,
            userId: auth?.user?.id
        };
        const request:GetBasketDetailsRequest = {
            filter: filter
        };
        const basketResult = await BasketService.GetBasketDetails(request);
        if(basketResult){
            basket.setBasketDetails(basketResult.details);
        } else {
            console.error('Ошибка на странице BasketPage');
        };
        
        const userId = auth?.user?.id;
        const ordersFilter:GetOrdersForUserFilter = {
            userId: userId,
            pageSize: 8,
            pageNumber: 1
        }
        const ordersRequest:GetOrdersForUserRequest = {
            filter:ordersFilter
        };

        const response = await OrderService.GetOrdersForUser(ordersRequest);
        if(response?.orders){
            orderStore.setOrders(response?.orders.items);
            orderStore.setTotalOrders(response?.totalCount);
        }
    }

    const submit: SubmitHandler<LoginRequest> = async (data: LoginRequest) => {
        try{
            setLoading(true);
            const response = await AuthService.isRegisterUser(data.email);
            if(response.isRegistered){
                data.type = 'login';
                await AuthService.sendVerificationCode(data.email);
                changeCodeVerifyStatus(data);
        }
        } catch(error){
            const errorResponse = error as ErrorResponseModel;
            setError(errorResponse.message ?? '');
        } finally{
            setLoading(false);
        }
    }
    
    return <div className={styles.container}>
        <form className={styles.form} onSubmit={handleSubmit(submit)}>
            <h2 className={styles.h2}>Вход</h2>
            <div>
                <label
                    className={styles.label} 
                    htmlFor='emailName'>E-mail: </label>
                <input id='emailName' type='text' className={styles.input} {...register('email', {
                    required: 'Введите почту'
                })} />
                {errors?.email && <div className={styles.error}>{errors.email?.message}</div>}
            </div>
            <div>
                <label
                    className={styles.label} 
                    htmlFor='password'>Пароль:</label>
                <input id='password' type='password' className={styles.input} {...register('password', {
                    required: 'Введите пароль',
                })} />
                {errors?.password && <div className={styles.error}>{errors.password?.message}</div>}
                {hasError && 
                <div className={styles.error}> 
                    {hasError} 
                </div>}
                <div className={styles.btnsContainer}>
                    <button className={styles.loginBtn} type='submit'>
                        {isLoading ? 
                            (<img src="/loading2.gif" className={styles.loading}/>)
                            : 
                            (<p className={styles.p}>
                                Войти
                            </p>)
                        }
                    </button>
                    <button onClick={handlerChangeFormStatus} type='button' className={styles.toRegisterBtn}>Ещё не зарегистрированы?</button>
                </div>
            </div>
        </form>
    </div>
}