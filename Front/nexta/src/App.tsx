import Header from './components/header/header'
import Footer from './components/footer/footer'
import "./globals.css"
import "./colors.css"
import { BrowserRouter, Route, Routes } from 'react-router-dom'
import AuthPage from './pages/auth/AuthPage'
import AccountPage from './pages/account/AccountPage'
import BasketPage from './pages/basket/BasketPage'
import HomePage from './pages/home/HomePage'
import { useEffect } from 'react'
import BasketDetailsFilter from './models/basket/BasketDetailsFilter'
import GetBasketDetailsRequest from './models/basket/GetBasketDetailsRequest'
import auth from './stores/auth'
import BasketService from './services/BasketService'
import basket from './stores/basket'

const App = () => {            
  useEffect(() => {
    const fetchData = async() => {
        const filter:BasketDetailsFilter = {
            pageNumber: 1,
            userId: auth?.user?.id
        };
        const request:GetBasketDetailsRequest = {
            filter: filter
        };
        const result = await BasketService.GetBasketDetails(request);
        if(result.statusCode == 200 && result.value){
            basket.setBasketDetails(result.value.details);
        } else {
            console.error('Ошибка на странице BasketPage');
        };
    }
    fetchData();
    }, []); 
  return <div className='page-container'>
      <BrowserRouter>
        <Header />
        <div className='page-body'>
          <Routes>
            <Route path="*" element={<HomePage />} />
            <Route path="Auth" element={<AuthPage />} />
            <Route path="Basket" element={<BasketPage />} />
            <Route path="Account" element={<AccountPage />} />
          </Routes>
        </div>
        <Footer />
      </BrowserRouter>
  </div>
}

export default App