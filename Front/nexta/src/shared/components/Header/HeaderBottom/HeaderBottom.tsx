import { Link, useNavigate } from 'react-router-dom';
import styles from './HeaderBottom.module.css';
import { HeaderExit } from '../HeaderNav/HeaderExit/HeaderExit';
import authStore from '../../../../stores/AuthStore/authStore';
import { observer } from 'mobx-react';
import { useCategoriesStore } from '../../../../stores/categoriesStore';
import { useSearchProductsStore } from '../../../../stores/SearchProductsStore/searchProductsStore';
import ProductsService from '../../../../services/ProductService';

export const HeaderBottom = observer(() => {
    const { categories } = useCategoriesStore();
    const { setProducts, setSearchTerm, setTotalPageCount, setCategory} = useSearchProductsStore();
    const navigator = useNavigate();

    const fetchData = async (query:string, category:string = '') => {
        const isAdmin = authStore.isAdmin;
        const response = await ProductsService.Get(query, category, 8, 1, isAdmin);
        if(response.success && response.status === 200){
            setSearchTerm(query);
            setProducts(response.data.data.items);
            setTotalPageCount(response.data.data.pageCount);
        }
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
                {categories.slice(0, 5).map(category => 
                    <button key={category.name} className={styles.headerItem} onClick={() => handleSearchOnCategory(category.name)}>
                        {category.name}
                    </button>)}
            </div>
            <div>
                Бесплатный подбор: +7 (915) 562-95-13
            </div>
            <div>
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