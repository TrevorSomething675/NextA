import { Detail } from "../../../details/models/Detail";
import styles from './detailFooter.module.css';

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