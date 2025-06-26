import { Detail } from "../../../../shared/entities/Detail";
import styles from './DetailHeader.module.css'

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