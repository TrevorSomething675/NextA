import Detail from '../../../models/Detail';
import styles from './detailFooter.module.css';

interface Props {
    detail:Detail
}

const DetailFooter:React.FC<Props> = ({detail}) => {
    return <div className={styles.container}>
        <button>
            Вернуться
        </button>
        <button>
            Заказать
        </button>
    </div>
}

export default DetailFooter;