import { useState } from "react";
import { AuthStep } from "../../../../../widgets/ui/auth-panel/models/AuthStep";
import { AuthData } from "../../../../../entities/auth/models/authData";
import styles from './AccessRecoveryFirstStep.module.css';
import { SubmitHandler, useForm } from "react-hook-form";
import { VerificationApi } from "../../../../../entities/auth/verification/api/verificationApi";
import { AccessRecoveryFirstStepRequest } from "../models/accessRecoveryFirstStepRequest";

interface AccessRecoveryFirstStepProps{
    handleChangeAuth: (step: AuthStep, user:AuthData) => void;
}

export const AccessRecoveryFirstStep:React.FC<AccessRecoveryFirstStepProps> = ({handleChangeAuth}) => {

    const { register, handleSubmit, formState: {errors} } = useForm<AccessRecoveryFirstStepRequest>();
    const [isLoading, setLoading] = useState(false);


    const submit: SubmitHandler<AccessRecoveryFirstStepRequest> = async (data: AccessRecoveryFirstStepRequest) => {
        try{
            setLoading(true);
            const response = await VerificationApi.SendVerificationCode(data.email);
            
            const authData: AuthData = {
                email: data.email,
                firstName: localStorage.getItem('firstName') ?? '',
                lastName: localStorage.getItem('lastName') ?? '',
                role: localStorage.getItem('role') ?? 'User'
            };

            if(response.success && response.status === 200){
                    handleChangeAuth('accessRecoverySecondStep', authData);
                }
            }
        finally{
            setLoading(false);
        }
    }

    return <div className={styles.container}>
        <form className={styles.form} onSubmit={handleSubmit(submit)}>
            <h2 className={styles.h2}>Восстановление доступа</h2>
            <div className={styles.body}>
                <label
                    className={styles.label}
                    htmlFor='emailName'>E-mail: </label>
                <input id='emailName' type='text' className={styles.input} {...register('email', {
                    required: 'Введите почту'
                })} />
                {errors?.email && <div className={styles.error}>{errors.email?.message}</div>}
            </div>
            <div>
                <div className={styles.btnsContainer}>
                    <button className={styles.submitBtn} type='submit'>
                        {isLoading ? 
                            (<img src="/loading2.gif" className={styles.loading}/>)
                            : 
                            (<p className={styles.p}>
                                Отправить код
                            </p>)
                        }
                    </button>
                </div>
            </div>
        </form>
    </div>
}