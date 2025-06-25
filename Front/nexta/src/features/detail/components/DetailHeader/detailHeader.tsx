import { Detail } from "../../../details/models/Detail";
import styles from './detailHeader.module.css';

const DetailHeader: React.FC<{detail:Detail}> = ({ detail }) => {
  return (
    <div className={styles.container}>
      <h2 className={styles.h2}>
        Товар {detail?.article}
      </h2>
    </div>
  );
};

export default DetailHeader;