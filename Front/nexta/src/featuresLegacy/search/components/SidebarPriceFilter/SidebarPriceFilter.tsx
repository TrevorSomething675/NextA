import { useSearchProductsStore } from '../../../../stores/SearchProductsStore/searchProductsStore';
import styles from './SidebarPriceFilter.module.css';

export const SidebarPriceFilter = () => {
    const { 
        priceFilters,
        setPriceFilters, setPage, setImmediatePriceUpdate
    } = useSearchProductsStore();

    const handleRadioChange = (range: string) => {
        let min, max;
        switch (range) {
            case 'under500':
                min = 1;
                max = 500;
                break;
            case '500-1500':
                min = 500;
                max = 1500;
                break;
            case '1500-6000':
                min = 1500;
                max = 6000;
                break;
            case 'over6000':
                min = 6001;
                max = 1000000;
                break;
            case 'any':
            default:
                min = 1;
                max = 1000000;
                range = 'any';
        }
        setPriceFilters({ min, max, range });
        setImmediatePriceUpdate();
        setPage(1);
    };

    return (
        <div className={styles.container}>
            <div className={styles.header}>
                <input
                    className={styles.priceInput}
                    value={priceFilters.min}
                    onChange={(e) =>
                        setPriceFilters({
                            ...priceFilters,
                            min: Number(e.target.value),
                            range: null
                        })
                    }
                    placeholder="от"
                />
                <input
                    className={styles.priceInput}
                    value={priceFilters.max}
                    onChange={(e) =>
                        setPriceFilters({
                            ...priceFilters,
                            max: Number(e.target.value),
                            range: null
                        })
                    }
                    placeholder="до"
                />
            </div>

            <div className={styles.priceFilter}>
                <input
                    className={styles.radio}
                    type="radio"
                    id="range-under500"
                    name="priceRange"
                    value="under500"
                    checked={priceFilters.range === 'under500'}
                    onChange={() => handleRadioChange('under500')}
                />
                <label htmlFor="range-under500">до 500 руб.</label>
            </div>

            <div className={styles.priceFilter}>
                <input
                    className={styles.radio}
                    type="radio"
                    id="range-500-1500"
                    name="priceRange"
                    value="500-1500"
                    checked={priceFilters.range === '500-1500'}
                    onChange={() => handleRadioChange('500-1500')}
                />
                <label htmlFor="range-500-1500">500-1500 руб.</label>
            </div>

            <div className={styles.priceFilter}>
                <input
                    className={styles.radio}
                    type="radio"
                    id="range-1500-6000"
                    name="priceRange"
                    value="1500-6000"
                    checked={priceFilters.range === '1500-6000'}
                    onChange={() => handleRadioChange('1500-6000')}
                />
                <label htmlFor="range-1500-6000">1500-6000 руб.</label>
            </div>

            <div className={styles.priceFilter}>
                <input
                    className={styles.radio}
                    type="radio"
                    id="range-over6000"
                    name="priceRange"
                    value="over6000"
                    checked={priceFilters.range === 'over6000'}
                    onChange={() => handleRadioChange('over6000')}
                />
                <label htmlFor="range-over6000">дороже 6000 руб.</label>
            </div>

            <div className={styles.priceFilter}>
                <input
                    className={styles.radio}
                    type="radio"
                    id="range-any"
                    name="priceRange"
                    value="any"
                    checked={priceFilters.range === 'any'}
                    onChange={() => handleRadioChange('any')}
                />
                <label htmlFor="range-any">любая</label>
            </div>
        </div>
    );
};
