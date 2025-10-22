import { useState } from 'react';
import styles from './Pagging.module.css';

const Pagging: React.FC<{pageCount: number, onPageNumberChange: (pageNumber: number) => void}> = ({pageCount, onPageNumberChange}) => {

    const pagesArray = Array.from({length: pageCount}, (_, index) => index + 1);
    const [page, setPage] = useState<number>(pagesArray[0])

    const ChangePageNumber = (pageNumber: number) =>{
        setPage(pageNumber);
        onPageNumberChange(pageNumber);
    }

    return <div className={styles.paggingContainer}>
        {(pagesArray?.length > 1) && pagesArray.map((pageNumber) => 
            <button key={pageNumber} className={styles.button} onClick={() => ChangePageNumber(pageNumber)}>
                {pageNumber}
            </button>
        )}
    </div>
}

export default Pagging;