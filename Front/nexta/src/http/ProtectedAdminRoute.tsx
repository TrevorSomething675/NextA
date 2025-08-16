import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { AuthService } from "../features/auth/services/AuthService";
import authStore from "../stores/AuthStore/authStore";

export const ProtectedAdminRoute = ({ children }: { children: React.ReactNode }) => {
    const [isAuthorized, setIsAuthorized] = useState(false);
    const navigate = useNavigate();

    useEffect(() => {
        const checkAdminAccess = async () => {
        try {
            const response = await AuthService.isAuth();
            setIsAuthorized(true);
        } catch (error) {
            authStore.setAdminStatus(false);
            console.warn(error);
            navigate('/Error');
        } finally {
            //navigate('/Error');
        }
        };

        checkAdminAccess();
    }, []);

    return isAuthorized ? children : null;
};