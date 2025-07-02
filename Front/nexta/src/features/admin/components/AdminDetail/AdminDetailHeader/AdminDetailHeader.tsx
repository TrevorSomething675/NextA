import { AdminDetail } from '../../../models/AdminDetail';
import styles from './AdminDetailHeader.module.css';

const AdminDetailHeader: React.FC<{detail:AdminDetail}> = ({ detail }) => {
    return (
        <div className={styles.container}>
            <h2 className={styles.h2}>
                Товар {detail?.article}
            </h2>
        </div>
    );
};

export default AdminDetailHeader;