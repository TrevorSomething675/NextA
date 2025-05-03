import styles from './login.module.css';
import { SubmitHandler, useForm } from 'react-hook-form';
import { useState } from 'react';
import auth from '../../stores/auth';
import LoginForm from '../../models/auth/Login';
import { useNavigate } from 'react-router-dom';

const Login:React.FC<{changeFormStatus:any}> = ({changeFormStatus}) => {
    const { register, handleSubmit } = useForm<LoginForm>();
    const [hasError, setError] = useState('');

    const handlerChangeFormStatus = () => {
        changeFormStatus();
    }

    const navigate = useNavigate();
    const submit: SubmitHandler<LoginForm> = async (data: LoginForm) => {
        const result = await auth.login(data);
        if(result?.statusCode == 200){
            navigate('/');
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