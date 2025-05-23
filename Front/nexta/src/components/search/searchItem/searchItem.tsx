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
            <div>
                {detail.name}
            </div>
        </div>
        <div className={styles.detailItem}>
            <div>
                {detail.article}
            </div>
        </div>
        <div className={styles.detailItem}>
            <div>
                {detail.description}
            </div>
        </div>
        <div className={styles.detailItem}>
            <div>
                Кол-во, шт: {detail.count}
            </div>
        </div>
        <div className={styles.detailItem}>
            <div>
                {detail.newPrice} руб.
            </div>
        </div>
    </div>
}

export default SearchItem;