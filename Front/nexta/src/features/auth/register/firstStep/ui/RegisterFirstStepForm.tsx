import { VerificationApi } from "../../../../../entities/auth/verification/api/verificationApi";
import { RegisterFirstStepRequest } from "../models/RegisterFirstStepRequest";
import { AuthStep } from "../../../../../widgets/ui/auth-panel/models/AuthStep";
import { AuthApi } from "../../../../../entities/auth/api/authApi";
import { SubmitHandler, useForm } from "react-hook-form";
import styles from './RegisterFirstStepForm.module.css';
import { useState } from "react";
import { AuthData } from "../../../../../entities/auth/models/authData";

interface RegisterFirstStepProps{
    changeAuth: (step:AuthStep, data?:AuthData) => void;
}

export const RegisterFirstStepForm:React.FC<RegisterFirstStepProps> = ({changeAuth}) => {
    const { register, handleSubmit, watch, formState: {errors} } = useForm<RegisterFirstStepRequest>();
    const [isLoading, setLoading] = useState(false);
    const [hasError, setError] = useState('');

    const handleToLogin = () => {
        changeAuth('login');
    }
    
    const submit:SubmitHandler<RegisterFirstStepRequest> = async(data: RegisterFirstStepRequest) => {
        try{
            setLoading(true);
            const response = await AuthApi.IsRegister(data.email);
            if(response.success && response.status === 200){
                if(response.data.exist){
                    await VerificationApi.SendVerificationCode(data.email);
                    changeAuth('registerSecondStep', );
                } else {
                    setError('Такой пользователь уже существует');
                }
            }
        }
        catch(error){
            setError('Ошибка сервера. Мы уже работает над её исправлением.');
        }
        finally{
            setLoading(false);
        }
    }

    return <div className={styles.container}>
            <form className={styles.form} onSubmit={handleSubmit(submit)}>
                <h2 className={styles.h2}>Регистрация</h2>
                <label className={styles.label} htmlFor='emailName'>E-mail: </label>
                <input 
                    id='emailName' 
                    type='text' 
                    className={styles.input} {...register('email', { 
                        required: 'Email обязателен',
                        pattern: {
                            value: /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i,
                            message: 'Некорректный email адрес'
                        }
                    })} />
                {errors.email && <div className={styles.error}>{errors.email?.message}</div>}
                
                <label className={styles.label} htmlFor='FirstName'>Имя: </label>
                <input 
                    id='FirstName' 
                    type='text' 
                    className={styles.input} {...register('firstName', {
                        pattern: {
                            value: /^[A-Za-zА-Яа-яЁё\s]+$/,
                            message: 'Имя должно содержать только буквы'
                        }
                    })} />
                {errors.firstName && <div className={styles.error}>{errors.firstName.message}</div>}

                <label className={styles.label} htmlFor='LastName'>Фамилия: </label>
                <input 
                    id='LastName' 
                    type='text' 
                    className={styles.input} {...register('lastName', {
                        pattern: {
                            value: /^[A-Za-zА-Яа-яЁё\s]+$/,
                            message: 'Фамилия должна содержать только буквы'
                        }
                    })} />
                {errors.lastName && <div className={styles.error}>{errors.lastName?.message}</div>}

                <label className={styles.label} htmlFor='MiddleName'>Отчество: </label>
                <input 
                    id='MiddleName'
                    type='text'
                    className={styles.input} {...register('middleName', {
                        pattern: {
                            value: /^[A-Za-zА-Яа-яЁё\s]+$/,
                            message: 'Отчество должно содержать только буквы'
                        }
                    })} />
                {errors.middleName && <div className={styles.error}>{errors.middleName?.message}</div>}

                <label className={styles.label} htmlFor='password'>Пароль:</label>
                <input 
                    id='password' 
                    type='password' 
                    className={styles.input} {...register('password', {
                        minLength: {
                            value: 6,
                            message: 'Пароль должен быть не менее 6 символов'
                        }
                    })} />
                {errors.password && <div className={styles.error}>{errors.password?.message}</div>}

                <label className={styles.label} htmlFor='confirmPassword'>Подтвердите пароль:</label>
                <input
                    id='confirmPassword'
                    type='password'
                    className={styles.input} {...register('confirmPassword', {
                        required: 'Подтвердите пароль',
                        validate: (value:string) => 
                            value === watch('password') || 'Пароли не совпадают'
                    })} />
                {errors.confirmPassword && <div className={styles.error}>{errors.confirmPassword?.message}</div>}
                
                {hasError && 
                <div className={styles.error}>
                    {hasError} 
                </div>}
                <div className={styles.btnsContainer}>
                    <button className={styles.registerBtn} type='submit'>
                        {isLoading ? 
                            (<img src="/loading2.gif" className={styles.loading}/>)
                            : 
                            (<p className={styles.p}>
                                Зарегистрироваться
                            </p>)
                        }
                    </button>
                    <button type='button' onClick={handleToLogin} className={styles.toLoginBtn}>Уже есть аккаунт?</button>
                </div>
            </form>
        </div>
}