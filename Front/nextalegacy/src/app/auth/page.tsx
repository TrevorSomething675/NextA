'use client'

import Register from "@/components/auth/register";
import Login from "@/components/auth/login";
import Footer from "@/components/footer/footer";
import Header from "@/components/header/header";

import { useState } from "react";

const DetailsPage = () => {
    const [isLogin, changeAuth] = useState(true);
    const loginStatusHandler = () => {
        changeAuth(!isLogin);
    }

    return <div className='page-container'>
        <Header />
        <div className='page-body'>
            {isLogin ? <Login changeFormStatus={loginStatusHandler} /> : <Register changeFormStatus={loginStatusHandler} />}
        </div>
        <Footer />
    </div>
}

export default DetailsPage;