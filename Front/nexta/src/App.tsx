import Header from './components/header/header'
import Footer from './components/footer/footer'
import "./globals.css"
import "./colors.css"
import { BrowserRouter, Route, Routes } from 'react-router-dom'
import { useEffect } from 'react'
import BasketDetailsFilter from './models/basket/BasketDetailsFilter'
import GetBasketDetailsRequest from './models/basket/GetBasketDetailsRequest'
import auth from './stores/auth'
import BasketService from './services/BasketService'
import basket from './stores/basket'
import DetailPage from './pages/detail/detailPage'
import SearchPage from './pages/search/searchPage'
import HomePage from './pages/home/homePage'
import AuthPage from './pages/auth/authPage'
import BasketPage from './pages/basket/basketPage'
import AccountPage from './pages/account/accountPage'
import OrderPage from './pages/order/orderPage'
import OrderService from './services/OrderService'
import orderStore from './stores/orderStore'
import GetOrdersForUserFilter from './models/order/GetOrdersForUserFilter'
import GetOrdersForUserRequest from './models/order/GetOrdersForUserRequest'
import { NotificationsProvider } from './components/notifications/notifications'

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
        const basketResult = await BasketService.GetBasketDetails(request);
        if(basketResult.statusCode == 200 && basketResult.value){
            basket.setBasketDetails(basketResult.value.details);
        } else {
            console.error('Ошибка на странице BasketPage');
        };
        
        const userId = auth?.user?.id;
        const ordersFilter:GetOrdersForUserFilter = {
            userId: userId,
            pageSize: 8,
            pageNumber: 1
        }
        const ordersRequest:GetOrdersForUserRequest = {
            filter:ordersFilter
        };

        const result = await OrderService.GetOrdersForUser(ordersRequest);
        if(result?.statusCode == 200){
            orderStore.setOrders(result?.value?.orders.items);
            orderStore.setTotalOrders(result?.value?.totalCount);
        }
    }
    fetchData();
    }, []); 
  return <div className='page-container'>
      <NotificationsProvider>
        <BrowserRouter>
          <Header />
          <div className='page-body'>
            <Routes>
              <Route path="*" element={<HomePage />} />
              <Route path="Auth" element={<AuthPage />} />
              <Route path="Basket" element={<BasketPage />} />
              <Route path="Search" element={<SearchPage />} />
              <Route path="Detail/:id" element={<DetailPage />} />
              <Route path="Account" element={<AccountPage />} />
              <Route path="Order" element={<OrderPage />} />
            </Routes>
          </div>
          <Footer />
        </BrowserRouter>
      </NotificationsProvider>
  </div>
}

export default App