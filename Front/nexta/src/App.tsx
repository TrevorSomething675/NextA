import Header from './components/header/header'
import Footer from './components/footer/footer'
import "./globals.css"
import "./colors.css"
import { BrowserRouter, Route, Routes } from 'react-router-dom'
import { useEffect } from 'react'
import auth from './stores/auth'
import basket from './stores/basket'
import DetailPage from './pages/detail/detailPage'
import SearchPage from './pages/search/searchPage'
import HomePage from './pages/home/homePage'
import OrderService from './services/OrderService'
import orderStore from './stores/orderStore'
import { NotificationsProvider } from './components/notifications/notifications'
import { GetBasketDetailsFilter, GetBasketDetailsRequest } from './features/basket/models/GetBasketDetails'
import { BasketService } from './features/basket/services/BasketService'
import { GetOrdersForUserFilter, GetOrdersForUserRequest } from './features/order/models/GetOrdersForUserFilter'
import AccountPage from './pages/account/AccountPage'
import AdminOrdersPage from './pages/admin/orders/AdminOrdersPage'
import { BasketPage } from './features/basket/pages/BasketPage'
import { AuthPage } from './features/auth/pages/AuthPage'
import OrderPage from './features/order/pages/OrdersPage'


const App = () => {
  useEffect(() => {
    auth.checkAuth()
    const fetchData = async() => {
        const filter:GetBasketDetailsFilter = {
            pageNumber: 1,
            userId: auth?.user?.id
        };
        const request:GetBasketDetailsRequest = {
            filter: filter
        };
        const basketResult = await BasketService.GetBasketDetails(request);
        if(basketResult){
            basket.setBasketDetails(basketResult.details);
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

        const orderResponse = await OrderService.GetOrdersForUser(ordersRequest);
        if(orderResponse !== undefined){
            orderStore.setOrders(orderResponse?.orders?.items);
            orderStore.setTotalOrders(orderResponse?.totalCount);
        }
      }
      if(auth.isAuth){
        fetchData();
      }
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
              <Route path="Admin/Orders" element={<AdminOrdersPage />} />
            </Routes>
          </div>
          <Footer />
        </BrowserRouter>
      </NotificationsProvider>
  </div>
}

export default App