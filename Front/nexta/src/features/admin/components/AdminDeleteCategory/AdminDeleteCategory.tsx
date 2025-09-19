import { SubmitHandler, useForm } from "react-hook-form";
import { DeleteCategoryRequest } from "../../../../http/models/categories/DeleteCategory";
import { useNotifications } from "../../../../shared/components/Notifications/Notifications";
import CategoryService from "../../../../services/CategoryService";
import styles from './AdminDeleteCategory.module.css';

export const AdminDeleteCategory = () => {
    const { register, handleSubmit, formState: {errors} } = useForm<DeleteCategoryRequest>();
    const {addNotification} = useNotifications();

    const submit: SubmitHandler<DeleteCategoryRequest> = async (data: DeleteCategoryRequest) => {
        const response = await CategoryService.Delete(data);

        if(response.success && response.status === 200){
            addNotification({
                header: 'Категория товара была успешно добавлена!'
            })
        }
    }

    return <div className={styles.container}>
        <form onSubmit={handleSubmit(submit)}>
            <div className={styles.body}>
                <label
                    className={styles.label}
                    htmlFor='name'>Удалить категорию: </label>
                <input id='name' type='text' className={styles.input} {...register('name', {
                    required: 'Введите название категории'
                })} />
                {errors?.name && <div className={styles.error}>{errors.name?.message}</div>}
            </div>
            <div className={styles.footer}>
                <button type='submit'>
                    Удалить
                </button>
            </div>
        </form>
    </div>
}