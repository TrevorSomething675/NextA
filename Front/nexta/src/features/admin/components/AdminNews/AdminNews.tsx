import { useEffect, useState } from "react";
import NewsService from "../../../../services/NewsService";
import { GetNewsResponse } from "../../../news/models/NewsResponse";
import Image from "../../../../shared/components/Image/Image";
import styles from './AdminNews.module.css';

const AdminNews = () => {
    
    const [newsResponse, setNewsResponse] = useState({} as GetNewsResponse);
    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {
        fetchData();
    }, []);

    const fetchData = async () =>{
        const news = await NewsService.GetNews();
        setNewsResponse(news);
        setIsLoading(false);
    }

    return <div className={styles.container}>
            <ul className={styles.ul}>
                {isLoading
                    ? Array(3).fill(0).map((_, index) => (
                        <li className={styles.newsItem} key={`loader-${index}`}>
                            <Image
                                isBase64Image={true} 
                                className={styles.loading}
                                isLoading={true} 
                            />
                        </li>
                      ))
                    : newsResponse.news?.map((news) => (
                        <li className={styles.newsItem} key={news?.image?.base64String}>
                            <Image
                                isBase64Image={true}
                                base64String={news?.image?.base64String}
                                className={styles.img}
                                isLoading={false}
                            />
                            <button className={styles.deleteBtn}>
                                Удалить
                            </button>
                        </li>
                    ))}
                </ul>
            </div>
}

export default AdminNews;