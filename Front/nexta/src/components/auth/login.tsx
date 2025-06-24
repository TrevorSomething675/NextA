import styles from './login.module.css';
import { SubmitHandler, useForm } from 'react-hook-form';
import { useState } from 'react';
import auth from '../../stores/auth';
import LoginForm from '../../models/auth/Login';
import BasketDetailsFilter from '../../models/basket/BasketDetailsFilter';
import GetBasketDetailsRequest from '../../models/basket/GetBasketDetailsRequest';
import BasketService from '../../services/BasketService';
import basket from '../../stores/basket';
import GetOrdersForUserFilter from '../../models/order/GetOrdersForUserFilter';
import GetOrdersForUserRequest from '../../models/order/GetOrdersForUserRequest';
import OrderService from '../../services/OrderService';
import orderStore from '../../stores/orderStore';
import ErrorResponseModel from '../../models/ErrorResponseModel';
import CodeService from '../../services/CodeService';

const Login:React.FC<{changeCodeVerifyStatus:any, changeAuthStatus:any}> = ({changeCodeVerifyStatus, changeAuthStatus}) => {
    const { register, handleSubmit, formState: {errors} } = useForm<LoginForm>();
    const [hasError, setError] = useState('');
    const [isLoading, setLoading] = useState(false);

    const handlerChangeFormStatus = () => {
        changeAuthStatus();
    }

    const fetchData = async() => {
        const filter:BasketDetailsFilter = {
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

    const submit: SubmitHandler<LoginForm> = async (data: LoginForm) => {
        try{
            setLoading(true);
            const result = await auth.login(data);
            if(result?.user){
                const email = localStorage.getItem('email') ?? '';
                await CodeService.SendVerificationCode(email);
                changeCodeVerifyStatus();
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

export default Login;