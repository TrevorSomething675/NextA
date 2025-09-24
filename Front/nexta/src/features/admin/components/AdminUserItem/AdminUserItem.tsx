import React, { useState, useEffect } from 'react';
import { useForm, SubmitHandler } from 'react-hook-form';

import styles from './AdminUserItem.module.css';
import { UpdateAccountRequest } from '../../../../http/models/account/UpdateAccount';
import { useNotifications } from '../../../../shared/components/Notifications/Notifications';
import { AdminUser } from '../../../../models/AdminUser';
import AccountService from '../../../../services/AccountService';
import AdminService from '../../../../services/AdminService';

export const AdminUserItem: React.FC<{ user: AdminUser; currentUserId?: string }> = ({
    user,
    currentUserId,
}) => {
    const { addNotification } = useNotifications();
    const [isEditing, setIsEditing] = useState(false);
    const [showDeleteConfirm, setShowDeleteConfirm] = useState(false);

    const {
        register,
        reset,
        handleSubmit,
    } = useForm<UpdateAccountRequest>({
        defaultValues: {
            id: user.id ?? '',
            firstName: user.firstName || '',
            lastName: user.lastName || '',
            middleName: user.middleName || '',
            email: user.email || '',
            phone: user.phone || '',
        },
    });

    useEffect(() => {
        reset({
            id: user.id ?? '',
            firstName: user.firstName || '',
            lastName: user.lastName || '',
            middleName: user.middleName || '',
            email: user.email || '',
            phone: user.phone || '',
        });
    }, [user, reset]);

    const onSubmit: SubmitHandler<UpdateAccountRequest> = async (data) => {
        const response = await AccountService.update(data);

        if (response.success && response.status === 200) {
            addNotification({
                header: 'Информация обновлена!'
            });
            setIsEditing(false);
        } else {
            addNotification({
                header: 'Ошибка при обновлении'
            });
        }
    };

    const handleDelete = async () => {
        const response = await AdminService.userDelete(user.id);

        if (response.success && response.status === 200) {
            addNotification({
                header: 'Пользователь удалён'
            });
        } else {
            addNotification({
                header: 'Не удалось удалить пользователя'
            });
        }

        setShowDeleteConfirm(false);
        setIsEditing(false);
    };

    const handleEdit = () => {
        setIsEditing(true);
    };

    const handleCancel = () => {
        reset();
        setIsEditing(false);
        setShowDeleteConfirm(false);
    };

    const isCurrentUser = currentUserId === user.id;

    return (
        <li className={styles.container}>
            {isEditing ? (
                <form onSubmit={handleSubmit(onSubmit)} className={styles.form}>
                    <input
                        {...register('firstName')}
                        placeholder="Имя"
                        className={styles.userItem}
                        autoFocus
                    />
                    <input
                        {...register('lastName')}
                        placeholder="Фамилия"
                        className={styles.userItem}
                    />
                    <input
                        {...register('middleName')}
                        placeholder="Отчество"
                        className={styles.userItem}
                    />
                    <input
                        {...register('email')}
                        placeholder="Email"
                        type="email"
                        className={styles.userItem}
                    />
                    <input
                        {...register('phone')}
                        placeholder="Телефон"
                        className={styles.userItem}
                    />

                    <div className={styles.actions}>
                        <button type="submit" className={styles.saveBtn}>
                            Сохранить
                        </button>
                        <button type="button" onClick={handleCancel} className={styles.cancelBtn}>
                            Отмена
                        </button>

                        {!isCurrentUser && (
                            <button
                                type="button"
                                onClick={() => setShowDeleteConfirm(true)}
                                className={styles.deleteBtn}
                            >
                                Удалить
                            </button>
                        )}
                    </div>

                    {showDeleteConfirm && (
                        <div className={styles.confirmDelete}>
                            <p>Вы уверены, что хотите удалить этого пользователя?</p>
                            <div className={styles.confirmActions}>
                                <button type="button" onClick={handleDelete} className={styles.confirmYes}>
                                    Да, удалить
                                </button>
                                <button type="button" onClick={() => setShowDeleteConfirm(false)} className={styles.confirmNo}>
                                    Нет
                                </button>
                            </div>
                        </div>
                    )}
                </form>
            ) : (
                <div className={styles.viewMode}>
                    <span className={styles.name}>
                        {user.lastName} {user.firstName} {user.middleName}
                    </span>
                    <span className={styles.email}>{user.email}</span>
                    <span className={styles.phone}>{user.phone || '—'}</span>
                    <div className={styles.controls}>
                        <button onClick={handleEdit} className={styles.editBtn}>
                            Редактировать
                        </button>

                        {!isCurrentUser && (
                            <button
                                onClick={() => setShowDeleteConfirm(true)}
                                className={styles.deleteBtn}
                            >
                                Удалить
                            </button>
                        )}
                    </div>

                    {showDeleteConfirm && (
                        <div className={styles.confirmDelete}>
                            <p>Удалить пользователя "{user.firstName} {user.lastName}"?</p>
                            <div className={styles.confirmActions}>
                                <button type="button" onClick={handleDelete} className={styles.confirmYes}>
                                    Да
                                </button>
                                <button type="button" onClick={() => setShowDeleteConfirm(false)} className={styles.confirmNo}>
                                    Нет
                                </button>
                            </div>
                        </div>
                    )}
                </div>
            )}
        </li>
    );
};