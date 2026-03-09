import { useEffect, useRef, useState } from "react";
import styles from './News.module.css';
import { News } from "../../../entities/news/news";
import { NewsApi } from "../../../shared/http/news/newsApi";
import { Image } from "../../../shared/ui";

export const NewsPage = () => {
    const [news, setNewsResponse] = useState<News[]>([]);
    const [isLoading, setIsLoading] = useState(true);
    const [currentSlide, setCurrentSlide] = useState(0);
    const sliderRef = useRef<HTMLUListElement>(null);
    const autoSlideInterval = useRef<number | null>(null);

    useEffect(() => {
        fetchData();
        return () => {
            stopAutoSlide();
        };
    }, []);

    useEffect(() => {
        if (!isLoading && news?.length) {
            startAutoSlide();
        }
        return () => {
            stopAutoSlide();
        };
    }, [isLoading, news, currentSlide]);

    const fetchData = async () => {
        const response = await NewsApi.Get();
        if(response.success && response.status === 200){
            setNewsResponse(response.data);
            setIsLoading(false);
        }
    };

    const stopAutoSlide = () => {
        if (autoSlideInterval.current) {
            clearInterval(autoSlideInterval.current);
            autoSlideInterval.current = null;
        }
    };

    const startAutoSlide = () => {
        stopAutoSlide();
        autoSlideInterval.current = window.setInterval(() => {
            nextSlide();
        }, 5000);
    };

    const nextSlide = () => {
        if (!news) return;
        setCurrentSlide(prev => 
            prev === Math.ceil(news.length / 3) - 1 ? 0 : prev + 1
        );
    };

    const prevSlide = () => {
        if (!news) return;
        setCurrentSlide(prev => 
            prev === 0 ? Math.ceil(news.length / 3) - 1 : prev - 1
        );
    };

    const handleSlideChange = (direction: 'prev' | 'next') => {
        direction === 'prev' ? prevSlide() : nextSlide();
        stopAutoSlide();
        startAutoSlide();
    };

    return (
        <div className={styles.container}>
            <button 
                className={styles.sliderButton} 
                onClick={() => handleSlideChange('prev')}
                aria-label="Previous news"
            >
                &lt;
            </button>
            
            <div className={styles.sliderWrapper}>
                <ul 
                    className={styles.ul} 
                    ref={sliderRef}
                    style={{
                        transform: `translateX(-${currentSlide * 100}%)`,
                        transition: 'transform 0.5s ease'
                    }}
                >
                    {isLoading
                        ? Array(3).fill(0).map((_, index) => (
                            <li className={styles.newsItem} key={`loader-${index}`}>
                                <div className={styles.imgLoading} />
                            </li>
                          ))
                        : news?.map((news, index) => (
                            <li 
                                className={styles.newsItem} 
                                key={news.id || index}
                            >
                                <div className={styles.imageContainer}>
                                    <Image
                                        isBase64Image={true}
                                        base64String={news?.base64String}
                                        className={styles.img}
                                        isLoading={false}
                                    />
                                </div>
                            </li>
                        ))}
                </ul>
            </div>
            
            <button 
                className={styles.sliderButton} 
                onClick={() => handleSlideChange('next')}
                aria-label="Next news"
            >
                &gt;
            </button>
        </div>
    );
};