import { useState } from 'react';
import styles from './Login.module.css';
import { SubmitHandler, useForm } from 'react-hook-form';
import { LoginRequest } from '../../../../http/models/auth/Login';
import { AuthService } from '../../../../services/AuthService';
import { ErrorResponseModel } from '../../../../shared/models/ErrorResponseModel';
import { AuthStep, UserData } from '../../pages/AuthPage';

interface RegisterSecondStepProps{
    handleChangeAuth: (step: AuthStep, user:UserData) => void;
}

const Login:React.FC<RegisterSecondStepProps> = ({handleChangeAuth}) => {
    const { register, handleSubmit, formState: {errors} } = useForm<LoginRequest>();
    const [hasError, setError] = useState('');
    const [isLoading, setLoading] = useState(false);

    const handleToRegister = () => {
        handleChangeAuth('registerFirstStep', {} as UserData);
    }

    const handleToFirstStepRecovery = () => {
        handleChangeAuth('accessRecoveryFirstStep', {} as UserData);
    }

    const submit: SubmitHandler<LoginRequest> = async (data: LoginRequest) => {
        try{
            setLoading(true);
            const response = await AuthService.login(data);
            if(response.success && response.status === 200){
                await AuthService.sendVerificationCode(data.email);
                const userData:UserData = {
                    id: response.data.user.id!,
                    email: response.data.user.email ?? '',
                    firstName: response.data.user.firstName ?? '',
                    lastName: response.data.user.lastName ?? '',
                    middleName: response.data.user.middleName ?? '',
                    password: data.password,
                };
                handleChangeAuth('codeVerify', userData);
            }
            else if (!response.success){
                setError(response.data.Message ?? '');
            } 
        } catch(error){
            const errorResponse = error as ErrorResponseModel;
            setError(errorResponse.Message ?? '');
        } finally{
            setLoading(false);
        }
    }
    
    return <div className={styles.container}>
        <form className={styles.form} onSubmit={handleSubmit(submit)}>
            <h2 className={styles.h2}>Авторизация</h2>
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
                <button className={styles.accessRecoveryBtn} onClick={handleToFirstStepRecovery}>Восстановить доступ</button>
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
                    <button onClick={handleToRegister} type='button' className={styles.toRegisterBtn}>Ещё не зарегистрированы?</button>
                </div>
            </div>
        </form>
    </div>
}

export default Login;