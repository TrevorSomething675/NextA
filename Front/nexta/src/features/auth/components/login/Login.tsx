import { useState } from 'react';
import styles from './Login.module.css';
import { SubmitHandler, useForm } from 'react-hook-form';
import { LoginRequest } from '../../models/login';
import { ErrorResponseModel } from '../../../../shared/models/ErrorResponseModel';
import { AuthService } from '../../services/AuthService';
import authStore from '../../../../stores/AuthStore/authStore';
import { AuthUser } from '../../../../stores/AuthStore/models/AuthUser';

const Login:React.FC<{changeAuthStatus:any, changeCodeVerifyStatus: (data: AuthUser) => void}> = ({changeAuthStatus, changeCodeVerifyStatus}) => {
    const { register, handleSubmit, formState: {errors} } = useForm<LoginRequest>();
    const [hasError, setError] = useState('');
    const [isLoading, setLoading] = useState(false);

    const handlerChangeFormStatus = () => {
        changeAuthStatus();
    }

    const submit: SubmitHandler<LoginRequest> = async (data: LoginRequest) => {
        try{
            setLoading(true);
            const response = await AuthService.login(data);
            if(response){
                await AuthService.sendVerificationCode(data.email);
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