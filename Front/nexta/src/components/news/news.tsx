import { useEffect, useState } from 'react';
import Image from '../image/Image';
import styles from './news.module.css';
import NewsService from '../../services/NewsService';
import NewsResponse from '../../models/news/newsResponse';

const News = () => {
    
    const [newsResponse, setNewsResponse] = useState({} as NewsResponse);
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
                    : newsResponse.images?.map((image) => (
                        <li className={styles.newsItem} key={image.base64String}>
                            <Image
                                isBase64Image={true}
                                base64String={image.base64String}
                                className={styles.img}
                                isLoading={false}
                            />
                        </li>
                ))}
            </ul>
        </div>
}

export default News;