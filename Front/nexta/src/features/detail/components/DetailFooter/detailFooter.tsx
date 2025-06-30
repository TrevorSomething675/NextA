import { GetDetailResponse } from "../../models/GetDetail";
import styles from './DetailFooter.module.css';

const DetailFooter:React.FC<{detail:GetDetailResponse}> = () => {
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