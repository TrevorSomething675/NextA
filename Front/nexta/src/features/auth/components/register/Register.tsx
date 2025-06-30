import { useState } from "react";
import styles from './Register.module.css';
import { RegistrationRequest } from "../../models/registration";
import { SubmitHandler, useForm } from "react-hook-form";
import { AuthService } from "../../services/AuthService";
import { ErrorResponseModel } from "../../../../shared/models/ErrorResponseModel";
import authStore from "../../../../stores/AuthStore/authStore";
import { AuthUser } from "../../../../stores/AuthStore/models/AuthUser";

const Register: React.FC<{ changeFormStatus:any, changeCodeVerifyStatus: (data: AuthUser) => void}> = ({ changeFormStatus, changeCodeVerifyStatus }) => {
    const { register, handleSubmit, watch, formState: {errors} } = useForm<RegistrationRequest>();
    const [hasError, setError] = useState('');

    const handlerChangeFormStatus = () => {
        changeFormStatus();
    }

    const submit: SubmitHandler<RegistrationRequest> = async (data: RegistrationRequest) => {
        try{
            if(data.password !== data.confirmPassword){
                setError('Пароли не совпадают');
            }
            const response = await AuthService.register(data);
            if(response){
                AuthService.sendVerificationCode(response.user.email!);
                const authUser:AuthUser = {
                    id: response.user.id,
                    email: response.user.email,
                    firstName: response.user.firstName,
                    lastName: response.user.lastName,
                    middleName: response.user.middleName,
                    role: response.user.role,
                    phone: response.user.phone,
                    accessToken: null
                };
                changeCodeVerifyStatus(authUser);
                authStore.firstStepAuthenticate(authUser);
            }
        } catch (error){
            const errorResponse = error as ErrorResponseModel;
            setError(errorResponse.message ?? '');
        }
    }

    return (
        <div className={styles.container}>
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
                <div> 
                    {hasError} 
                </div>}
                <div className={styles.btnsContainer}>
                    <button className={styles.registerBtn} type='submit'>Зарегистрироваться</button>
                    <button type='button' onClick={handlerChangeFormStatus} className={styles.toLoginBtn}>Уже есть аккаунт?</button>
                </div>
            </form>
        </div>
    );
}

export default Register;