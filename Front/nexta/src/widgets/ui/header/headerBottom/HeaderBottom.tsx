import { useSearchProductsStore } from "../../../../shared/stores/searchProduct/searchProductsStore";
import { useCategoriesStore } from "../../../../shared/stores/categories/categoriesStore";
import { HeaderExit } from "../headerNav/headerExit/HeaderExit";
import authStore from "../../../../shared/stores/auth/authStore";
import { Link, useNavigate } from "react-router-dom";
import styles from './HeaderBottom.module.css';
import { observer } from "mobx-react";

export const HeaderBottom = observer(() => {
    const { categories } = useCategoriesStore();
    const { setCategory} = useSearchProductsStore();
    const navigator = useNavigate();

    const fetchData = async (query:string, category:string = '') => {
        const isAdmin = authStore.isAdmin;
    };

    const handleSearchOnCategory = async(category:string) => {
        setCategory(category);
        await fetchData('', category);
        goToSearchPage();
    }

    const goToSearchPage = () => {
        navigator('/Search');
    }

    return <div className={styles.container}>
        <div className={styles.header}>
            <div className={styles.categories}>
                {categories?.slice(0, 5).map(category => 
                    <button key={category.name} className={styles.headerItem} onClick={() => handleSearchOnCategory(category.name)}>
                        {category.name}
                    </button>)}
            </div>
            <div>
                Бесплатный подбор: +7 (915) 562-95-13
            </div>
            <div className={styles.headerRightPanel}>
                {authStore.isAuthenticated &&
                    <>
                        <Link to='/Account' className={styles.headerItem}>
                            Личный кабинет
                        </Link>
                        <Link to='/' className={styles.headerItem}>
                            <HeaderExit />
                        </Link>
                    </>
                }
            </div>
        </div>
    </div>
});