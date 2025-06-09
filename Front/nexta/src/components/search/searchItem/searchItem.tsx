import { useNavigate } from 'react-router-dom';
import Detail from '../../../models/Detail';
import styles from './searchItem.module.css';

const SearchItem:React.FC<{detail:Detail}> = ({detail}) =>{
    const navigate = useNavigate();
    const goToDetailPage = () => {
        navigate(`/Detail/${detail.id}`);
    };

    return <div className={styles.container} onClick={goToDetailPage}>
        <div className={styles.detailItem}>
            {detail.name}
        </div>
        <div className={styles.detailItem}>
            {detail.article}
        </div>
        <div className={styles.detailItemDescription}>
            {detail.description}
        </div>
        <div className={styles.detailItem}>
            Кол-во, шт: {detail.count}
        </div>
        <div className={styles.detailItem}>
            <span className={detail.oldPrice ? styles.newPrice : styles.defaultPrice}>
                {detail.newPrice} руб.
            </span>
            {(detail.oldPrice !== undefined && detail.oldPrice != 0) &&
                <span className={styles.oldPrice}>
                    {detail.oldPrice} руб.
                </span>
            }
        </div>
    </div>
}

export default SearchItem;