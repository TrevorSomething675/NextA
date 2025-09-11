import { useEffect, useRef, useState } from "react";
import SearchSvg from "../../../../shared/svgs/SearchSvg/SearchSvg";
import styles from './AdminOrderSearch.module.css';
import AdminOrderService from "../../../../services/AdminOrderService";
import { GetAdminOrdersRequest, GetAdminOrdersResponse } from "../../../../http/models/adminOrders/GetAdminOrders";
import { UserOrder } from "../../../../models/order/UserOrder";

interface OrderSearchProps {
    onResponseChange: (orders: UserOrder[]) => void;
}

export const AdminOrderSearch: React.FC<OrderSearchProps> = ({ onResponseChange }) => {
    const [isLoading, setLoading] = useState(false);
    const debounceTimeout = useRef<null | number>(null);
    const [response, setResponse] = useState<GetAdminOrdersResponse>({} as GetAdminOrdersResponse);
    const [selectedStatuses, setSelectedStatuses] = useState<number[]>([0, 1, 2, 3, 4]);
    const [searchQuery, setSearchQuery] = useState('');
    const [showStatusDropdown, setShowStatusDropdown] = useState(false);

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setLoading(true);
        const value = e.target.value;
        setSearchQuery(value);

        if (debounceTimeout.current) {
            clearTimeout(debounceTimeout.current);
        }

        debounceTimeout.current = window.setTimeout(() => {
            fetchData(value, selectedStatuses);
        }, 1000);
    };

    const handleStatusChange = (status: number, isChecked: boolean) => {
        const newSelectedStatuses = isChecked
            ? [...selectedStatuses, status]
            : selectedStatuses.filter(s => s !== status);
        
        setSelectedStatuses(newSelectedStatuses);
        triggerFetch(searchQuery, newSelectedStatuses);
    };

    const triggerFetch = (query: string, statuses: number[]) => {
        setLoading(true);
        
        if (debounceTimeout.current) {
            clearTimeout(debounceTimeout.current);
        }

        debounceTimeout.current = window.setTimeout(() => {
            fetchData(query, statuses);
        }, 1000);
    };

    const fetchData = async (query: string, statuses: number[]) => {
        try {
            const request: GetAdminOrdersRequest = {
                searchTerm: query ?? '',
                pageSize: 8,
                statuses: statuses,
                pageNumber: 1
            };
            const response = await AdminOrderService.GetOrders(request);
            if(response.success && response.status === 200){
                setResponse(response.data);
                onResponseChange(response.data.data.items);
            }
        } catch (error) {
            console.error('Ошибка при получении данных:', error);
        }
        setLoading(false);
    };

    useEffect(() => {
        fetchData('', selectedStatuses);
    }, []);
    
    const statusOptions = [
    { value: 0, label: 'Принят', color: '#1c6cb8' },
    { value: 1, label: 'В работе', color: '#ed7e00' },
    { value: 2, label: 'Отменён', color: '#850f16' },
    { value: 3, label: 'Готов к выдаче', color: '#1e7309' },
    { value: 4, label: 'Завершён', color: 'gray' }
    ];

    return (
        <div>
            <div className={styles.container}>
                <button className={styles.searchButton}>
                    <SearchSvg />
                </button>
                <input
                    className={styles.searchInput}
                    placeholder="Поиск по ФИО, Id заказа"
                    onChange={handleInputChange}
                    value={searchQuery}
                />
                <div className={styles.loadingContainer}>
                    {isLoading && <img src="/loading.gif" className={styles.loading} />}
                </div>
            </div>
            <div className={styles.filterContainer}>
                <h2 className={styles.h2}>Фильтры:</h2>
                <div className={styles.statusDropdown}>
                    <div 
                        className={styles.statusDropdownHeader}
                        onClick={() => setShowStatusDropdown(!showStatusDropdown)}
                    >
                        Статусы заказов
                        <span className={showStatusDropdown ? styles.arrowUp : styles.arrowDown}></span>
                    </div>
                    {showStatusDropdown && (
                        <div className={styles.statusDropdownContent}>
                            {statusOptions.map(status => (
                                <label key={status.value} className={styles.statusOption}>
                                    <input
                                        type="checkbox"
                                        checked={selectedStatuses.includes(status.value)}
                                        onChange={(e) => handleStatusChange(status.value, e.target.checked)}
                                    />
                                    <span 
                                        className={styles.statusBadge}
                                        style={{ backgroundColor: status.color }}
                                    ></span>
                                    {status.label}
                                </label>
                            ))}
                        </div>
                    )}
                </div>
            </div>
        </div>
    );
};