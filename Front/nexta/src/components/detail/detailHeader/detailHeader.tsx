import Detail from '../../../models/Detail';
import styles from './detailHeader.module.css';

interface Props {
  detail: Detail;
}

const DetailHeader: React.FC<Props> = ({ detail }) => {
  return (
    <div className={styles.container}>
      <h2 className={styles.h2}>
        Товар {detail?.article}
      </h2>
    </div>
  );
};

export default DetailHeader;