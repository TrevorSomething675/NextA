import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import authStore from "../stores/AuthStore/authStore";
import { AuthService } from "../services/AuthService";

export const ProtectedAdminRoute = ({ children }: { children: React.ReactNode }) => {
    const [isAuthorized, setIsAuthorized] = useState(false);
    const navigate = useNavigate();

    useEffect(() => {
        const checkAdminAccess = async () => {
        try {
            const response = await AuthService.checkAuth();
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