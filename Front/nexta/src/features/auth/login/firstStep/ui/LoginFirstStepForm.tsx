import { SubmitHandler, useForm } from 'react-hook-form';
import styles from './LoginFirstStepForm.module.css';
import Button from '../../../../../shared/ui/Button/Button';
import { useState } from 'react';
import { AuthApi } from '../../../../../entities/auth/api/authApi';
import { AuthStep } from '../../../../../widgets/ui/auth-panel/models/AuthStep';
import { AuthData } from '../../../../../entities/auth/models/authData';
import { LoginFormRequest } from '../models/LoginFirstStepFormRequest';

interface LoginFormProps {
    changeAuth: (step:AuthStep, data?:AuthData) => void;
}

export const LoginFirstStepForm:React.FC<LoginFormProps> = ({changeAuth}) => {

    const {register, handleSubmit, formState: {errors}} = useForm<LoginFormRequest>();
    const [isLoading, setLoading] = useState(false);
    const [hasError, setError] = useState('');

    const handleToRegister = () => {
        changeAuth('registerFirstStep');
    }

    const handleToFirstStepRecovery = () => {
        changeAuth('accessRecoveryFirstStep');
    }

    const submit: SubmitHandler<LoginFormRequest> = async(data:LoginFormRequest) => {
        try{
            setLoading(true);
            const response = await AuthApi.Login(data.email, data.password);
            if(response.success && response.status === 200){
                const data:AuthData = {
                    firstName:response.data.firstName!,
                    middleName:response.data.middleName ?? '',
                    lastName:response.data.lastName!,
                    email:response.data.email!,
                    phone:response.data.phone ?? '',
                    role:response.data.role
                }
                changeAuth('loginSecondStep', data);
            } else if(!response.success && response.status !== 200){
                setError(response.data.Message ?? '');
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
                    <Button className={styles.loginBtn} type='submit'>
                        {isLoading ? 
                            (<img src="/loading2.gif" className={styles.loading}/>)
                            : 
                            (<p className={styles.p}>
                                Войти
                            </p>)
                        }
                    </Button>
                    <button onClick={handleToRegister} type='button' className={styles.toRegisterBtn}>Ещё не зарегистрированы?</button>
                </div>
            </div>
        </form>
    </div>
}