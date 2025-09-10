import NewsService from "../../../../services/NewsService";
import { GetNewsResponse } from "../../../../http/models/adminNews/GetNews";
import Image from "../../../../shared/components/Image/Image";
import styles from './AdminNews.module.css';
import { useNotifications } from "../../../../shared/components/Notifications/Notifications";

interface AdminNewsProps {
    newsResponse: GetNewsResponse;
    isLoading: boolean;
    onNewsDelete: (id: string) => void;
}

const AdminNews: React.FC<AdminNewsProps> = ({ 
    newsResponse, 
    isLoading, 
    onNewsDelete
}) => {
    const { addNotification } = useNotifications();

    const handleDelete = async (id: string) => {
        const response = await NewsService.Delete(id);
        if(response.success && response.status === 200){
            addNotification({
                'header': 'Новость успешно удалена'
            });
            onNewsDelete(id);
        }
    }

    return (
        <div className={styles.container}>
            <div className={styles.newsGrid}>
                {isLoading
                    ? Array(3).fill(0).map((_, index) => (
                        <div className={styles.newsItem} key={`loader-${index}`}>
                            <Image
                                isBase64Image={true} 
                                className={styles.loading}
                                isLoading={isLoading} 
                            />
                        </div>
                      ))
                    : newsResponse.news.map((news) => (
                        <div className={styles.newsItem} key={news.id}>
                            <Image
                                isBase64Image={true}
                                base64String={news?.image?.base64String}
                                className={styles.img}
                                isLoading={false}
                            />
                            <button 
                                className={styles.deleteBtn} 
                                onClick={() => handleDelete(news.id)}
                            >
                                Удалить
                            </button>
                        </div>
                    ))}
            </div>
        </div>
    );
};

export default AdminNews;