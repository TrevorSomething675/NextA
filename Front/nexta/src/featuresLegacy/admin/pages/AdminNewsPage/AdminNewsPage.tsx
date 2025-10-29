import { useEffect, useState } from 'react';
import AdminNews from '../../components/AdminNews/AdminNews';
import CreateNews from '../../components/CreateNews/CreateNews';
import styles from './AdminNewsPage.module.css';
import { NewsApi } from '../../../../shared/http/news/newsApi';
import { News } from '../../../../entities/news/models/news';

const AdminNewsPage = () => {
    const [newsResponse, setNewsResponse] = useState<News[]>([]);
    const [isLoading, setIsLoading] = useState(true);

    const fetchData = async () => {
        const response = await NewsApi.Get();
        if(response.success && response.status === 200){
            setNewsResponse(response.data);
            setIsLoading(false);
        }
    }

    const handleNewsDelete = (id: string) => {
        setNewsResponse(prev => ({
            ...prev,
            news: prev?.filter(newsItem => newsItem.id !== id) || []
        }));
    }

    useEffect(() => {
        fetchData();
    }, []);

    return <div>
        <h2 className={styles.h2}>Создать новость</h2>
        <div>
            <CreateNews fetchData={fetchData} />
        </div>
        <h2 className={styles.h2}>Новости</h2>
        <div>
            <AdminNews
                newsResponse={newsResponse}
                isLoading={isLoading}
                onNewsDelete={handleNewsDelete}
            />
        </div>
    </div>
}

export default AdminNewsPage;