import styles from './login.module.css';
import { SubmitHandler, useForm } from 'react-hook-form';
import { useState } from 'react';
import auth from '../../stores/auth';
import LoginForm from '../../models/auth/Login';
import { useNavigate } from 'react-router-dom';
import BasketDetailsFilter from '../../models/basket/BasketDetailsFilter';
import GetBasketDetailsRequest from '../../models/basket/GetBasketDetailsRequest';
import BasketService from '../../services/BasketService';
import basket from '../../stores/basket';
import GetOrdersForUserFilter from '../../models/order/GetOrdersForUserFilter';
import GetOrdersForUserRequest from '../../models/order/GetOrdersForUserRequest';
import OrderService from '../../services/OrderService';
import orderStore from '../../stores/orderStore';

const Login:React.FC<{changeFormStatus:any}> = ({changeFormStatus}) => {
    const { register, handleSubmit } = useForm<LoginForm>();
    const [hasError, setError] = useState('');

    const handlerChangeFormStatus = () => {
        changeFormStatus();
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
        if(basketResult.statusCode == 200 && basketResult.value){
            basket.setBasketDetails(basketResult.value.details);
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

        const result = await OrderService.GetOrdersForUser(ordersRequest);
        if(result?.statusCode == 200){
            orderStore.setOrders(result?.value?.orders.items);
            orderStore.setTotalOrders(result?.value?.totalCount);
        }
    }

    const navigate = useNavigate();
    const submit: SubmitHandler<LoginForm> = async (data: LoginForm) => {
        const result = await auth.login(data);
        if(result?.statusCode == 200){
            navigate('/');
            fetchData();
        } else{
            setError(result?.errorMessages.join(', ')!);
        }
    }
    
    return <div className={styles.container}> 
        <form className={styles.form} onSubmit={handleSubmit(submit)}>
            <h2 className={styles.h2}>Вход</h2>
            <div>
                <label
                    className={styles.label} 
                    htmlFor='emailName'>E-mail: </label>
                <input id='emailName' type='text' className={styles.input} {...register('email')} />
            </div>
            <div>
                <label
                    className={styles.label} 
                    htmlFor='password'>Пароль:</label>
                <input id='password' type='password' className={styles.input} {...register('password')} />
                {hasError && 
                <div> 
                    {hasError} 
                </div>}
                <div className={styles.btnsContainer}>
                    <button className={styles.loginBtn} type='submit'>Войти</button>
                    <button onClick={handlerChangeFormStatus} type='button' className={styles.toRegisterBtn}>Ещё не зарегистрированы?</button>
                </div>
            </div>
        </form>
    </div>
}

export default Login;