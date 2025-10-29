import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import authStore from "../shared/stores/auth/authStore";
import { AuthApi } from "../shared/http/auth/authApi";

export const ProtectedAdminRoute = ({ children }: { children: React.ReactNode }) => {
    const [isAuthorized, setIsAuthorized] = useState(false);
    const navigate = useNavigate();

    useEffect(() => {
        const checkAdminAccess = async () => {
            try {
                const response = await AuthApi.CheckAuth(authStore?.user?.email!, authStore?.user?.role ?? 'User');
                if(response.success && response.status === 200){
                    setIsAuthorized(true);
                }
            } catch (error) {
                authStore.setAdminStatus(false);
                navigate('/Error');
            } finally {
                //navigate('/Error');
            }
        };

        checkAdminAccess();
    }, []);

    return isAuthorized ? children : null;
};