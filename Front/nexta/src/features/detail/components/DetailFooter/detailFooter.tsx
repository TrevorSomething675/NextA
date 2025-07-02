import { Detail } from "../../../../shared/entities/Detail";
import styles from './DetailFooter.module.css';

const DetailFooter:React.FC<{detail:Detail}> = () => {
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