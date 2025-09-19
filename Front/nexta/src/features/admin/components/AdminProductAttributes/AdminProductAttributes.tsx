import styles from './AdminProductAttributes.module.css';
import React from 'react';

interface ProductAttribute {
    productId: string;
    key: string;
    value: string;
}

interface AdminProductAttributesProps {
    attributes: ProductAttribute[];
    productId: string;
    onChange: (updatedAttributes: ProductAttribute[]) => void;
}

export const AdminProductAttributes: React.FC<AdminProductAttributesProps> = ({
    attributes,
    productId,
    onChange,
}) => {
    const handleAttributeChange = (
        index: number,
        field: 'key' | 'value',
        value: string
    ) => {
        const newAttributes = attributes.map((attr, i) =>
            i === index ? { ...attr, [field]: value } : { ...attr }
        );
        onChange(newAttributes);
    };

    const handleRemoveAttribute = (index: number) => {
        const newAttributes = attributes.filter((_, i) => i !== index);
        onChange(newAttributes);
    };

    const handleAddAttribute = () => {
        const newAttribute: ProductAttribute = {
            productId,
            key: 'Новая характеристика',
            value: '',
        };
        onChange([...attributes, newAttribute]);
    };

    return (
        <div className={styles.container}>
            {attributes.length === 0 ? (
                <p>Нет характеристик</p>
            ) : (
                <ul className={styles.list}>
                    {attributes.map((attr, index) => (
                        <li key={index} className={styles.attributeItem}>
                            {/* Поле для редактирования ключа */}
                            <input
                                type="text"
                                value={attr.key}
                                onChange={(e) =>
                                    handleAttributeChange(index, 'key', e.target.value)
                                }
                                placeholder="Ключ"
                                className={styles.keyInput}
                            />

                            <span className={styles.dots}></span>

                            {/* Поле для редактирования значения */}
                            <input
                                type="text"
                                value={attr.value}
                                onChange={(e) =>
                                    handleAttributeChange(index, 'value', e.target.value)
                                }
                                placeholder="Значение"
                                className={styles.input}
                            />

                            {/* Кнопка удаления */}
                            <button
                                type="button"
                                onClick={() => handleRemoveAttribute(index)}
                                aria-label="Удалить характеристику"
                                className={styles.removeButton}
                            >
                                ×
                            </button>
                        </li>
                    ))}
                </ul>
            )}

            <button type="button" onClick={handleAddAttribute} className={styles.addButton}>
                + Добавить характеристику
            </button>
        </div>
    );
};