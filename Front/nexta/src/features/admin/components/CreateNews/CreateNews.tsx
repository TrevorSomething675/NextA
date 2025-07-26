import { useState } from "react";
import Image from "../../../../shared/components/Image/Image";
import styles from './CreateNews.module.css';
import { SubmitHandler, useForm } from "react-hook-form";
import { AddNewsForm } from "../../models/AddNews/AddNews";
import AdminService from "../../../../services/AdminService";
import { useNotifications } from "../../../../shared/components/Notifications/Notifications";

const CreateNews = () => {
    const { register, setValue, handleSubmit, formState: {errors}} = useForm<AddNewsForm>();
    const [previewImage, setPreviewImage] = useState<string | null>(null);
    const { addNotification } = useNotifications();
    const [error, setError] = useState<string>('');

    const handleOnChangeImage = (e: React.ChangeEvent<HTMLInputElement>) => {
        const file = e.target.files?.[0]
        if(!file) return;

        const name = file.name;
        const reader = new FileReader();
        reader.onload = (event) => {
            const base64String = event.target?.result?.toString().split(',')[1];
            setPreviewImage(base64String!);
            setValue('image.base64string', base64String);
            setValue('image.name', name);
        }
        reader.readAsDataURL(file);
    }
    const submit: SubmitHandler<AddNewsForm> = async (data:AddNewsForm) => {
        try{
            if(data.image === undefined) {
                setError('Необходимо добавить картинку для новости');
                return;
            }
            data.image.bucket = 'news';
            if(data.image === null){
                setError('Картинка не должна быть пустой');
                return;
            }
            setError('');
            AdminService.AddNews(data)
                .then(result => {
                    addNotification({
                        header: 'Новость успешно добавлена'
                    })
                    setPreviewImage('');
                })
                .catch(error => {
                    setError(error.Message);
                });
        }
        catch(error){
            console.error(error);
        }
    }

    return <div className={styles.container}>
        <form className={styles.form} onSubmit={handleSubmit(submit)}>
            <div className={styles.imageContainer}>
                <Image isLoading={false} base64String={previewImage ?? undefined} isBase64Image={true} className={styles.image} />
                <input className={styles.imageInput}
                    type="file"
                    accept="image/*"
                    onChange={handleOnChangeImage}
                />
            </div>
            <div className={styles.newsItems}>
                <div>
                    <label>Заголовок: </label>
                    <input className={styles.input} {...register('header')} />
                </div>
                <div className={styles.textAreaItem}>
                    <label>Тело: </label>
                    <textarea className={styles.textArea} {...register('description')} />
                </div>
                <div>
                    {error &&
                        <p className={styles.error}>
                            {error}
                        </p>
                    }
                </div>
                <div className={styles.sumbitContainer}>
                    <button type="submit" className={styles.submitBtn}>Создать</button>
                </div>
            </div>
        </form>
    </div>
}

export default CreateNews;