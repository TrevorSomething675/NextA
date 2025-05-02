import { SubmitHandler, useForm } from 'react-hook-form';
import styles from './register.module.css';
import RegisterForm from '../../models/auth/Register';
import auth from '../../stores/auth';
import { useNavigate } from 'react-router-dom';

const Register: React.FC<{ changeFormStatus: any }> = ({ changeFormStatus }) => {
    const { register, handleSubmit } = useForm<RegisterForm>();

    const handlerChangeFormStatus = () => {
        changeFormStatus();
    }

    const navigate = useNavigate();

    const submit: SubmitHandler<RegisterForm> = async (data: RegisterForm) => {
        await auth.register(data);
        navigate('/');
    }

    return (
        <div className={styles.container}>
            <form className={styles.form} onSubmit={handleSubmit(submit)}>
                <h2 className={styles.h2}>Регистрация</h2>
                <label className={styles.label} htmlFor='emailName'>E-mail: </label>
                <input id='emailName' type='text' className={styles.input} {...register('email')} />
                
                <label className={styles.label} htmlFor='FirstName'>Имя: </label>
                <input id='FirstName' type='text' className={styles.input} {...register('firstName')} />

                <label className={styles.label} htmlFor='LastName'>Фамилия: </label>
                <input id='LastName' type='text' className={styles.input} {...register('lastName')} />

                <label className={styles.label} htmlFor='MiddleName'>Отчество: </label>
                <input id='MiddleName' type='text' className={styles.input} {...register('middleName')} />

                <label className={styles.label} htmlFor='password'>Пароль:</label>
                <input id='password' type='password' className={styles.input} {...register('password')} />

                <label className={styles.label} htmlFor='confirmPassword'>Подтвердите пароль:</label>
                <input id='confirmPassword' type='password' className={styles.input} {...register('confirmPassword')} />
                
                <div className={styles.btnsContainer}>
                    <button className={styles.registerBtn} type='submit'>Зарегистрироваться</button>
                    <button type='button' onClick={handlerChangeFormStatus} className={styles.toLoginBtn}>Уже есть аккаунт?</button>
                </div>
            </form>
        </div>
    );
}

export default Register;