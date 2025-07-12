import { useState } from "react";
import Image from "../../../../shared/components/Image/Image";
import styles from './CreateNews.module.css';
import { AdminNewsResponse } from "../../models/News/AdminNewsResponse";

const CreateNews = () => {
    const [image, setImage] = useState<AdminNewsResponse>({
        image: {
            name: '',
            base64String: '',
        }
    });

    return <div className={styles.container}>
        <form className={styles.form}>
            <div className={styles.imageContainer}>
                <Image isLoading={false} isBase64Image={true} className={styles.image} />
                <input className={styles.imageInput}
                    type="file" 
                    accept="image/*"
                />
            </div>
            <div className={styles.newsItems}>
                <div>
                    <label>Заголовок: </label>
                    <input className={styles.input} name="header" />
                </div>
                <div className={styles.textAreaItem}>
                    <label>Тело: </label>
                    <textarea className={styles.textArea} name="description" />
                </div>
            </div>
        </form>
    </div>
}

export default CreateNews;