import "./globals.css"
import "./colors.css"
import { BrowserRouter, Route, Routes } from 'react-router-dom'
import { useEffect } from 'react'
import OrderService from './services/OrderService'
import orderStore from './stores/orderStore'
import { NotificationsProvider } from "./shared/components/Notifications/Notifications"
import HomePage from "./features/home/pages/HomePage"
import BasketPage from "./features/basket/pages/BasketPage"
import SearchPage from "./features/search/pages/SearchPage/SearchPage"
import OrderPage from "./features/order/pages/OrdersPage"
import BasketService from "./services/BasketService"
import {Header} from "./shared/components/Header/Header"
import Footer from "./shared/components/Footer/Footer"
import AccountPage from "./features/account/pages/AccountPage"
import AdminOrdersPage from "./features/admin/pages/AdminOrdersPage/AdminOrdersPage"
import AdminNewsPage from "./features/admin/pages/AdminNewsPage/AdminNewsPage"
import { ErrorPage } from "./features/error/pages/ErrorPage"
import { ProtectedAdminRoute } from "./http/ProtectedAdminRoute"
import { ProductPage } from "./features/product/pages/ProductPage/ProductPage"
import { AuthPage } from "./features/auth/pages/AuthPage"
import { AuthService } from "./services/AuthService"
import authStore from "./stores/AuthStore/authStore"
import basket from "./stores/basket"
import { AdminProductPage } from "./features/admin/pages/AdminProductPage/AdminProductPage"
import { AdminProductsPage } from "./features/admin/pages/AdminProductsPage/AdminProductsPage"
import { BasketSidebar } from "./features/basket/components/BasketSidebar/BasketSidebar"
import { observer } from "mobx-react"
import { AdminCategoryPage } from "./features/admin/pages/AdminCategoryPage/AdminCategoryPage"
import CategoryService from "./services/CategoryService"
import { useCategoriesStore } from "./stores/categoriesStore"
import { AdminUsersPage } from "./features/admin/pages/AdminUsersPage/AdminUsersPage"
import { HeaderTop } from "./shared/components/Header/HeaderTop/HeaderTop"

const App = observer(() => {
  const { setCategories } = useCategoriesStore();
  useEffect(() => {
    const fetchData = async() => {
      const authResponse = await AuthService.checkAuth();
      if(authResponse.success && authResponse.status === 200){
        authStore.setUserData(authResponse.data.user);
      } else {
        await AuthService.logout();
      }

      const userId = authStore.user.id ?? '';
      const basketResponse = await BasketService.GetBasketProducts(userId);
      if(basketResponse.success && basketResponse.status === 200){
        basket.setBasketItems(basketResponse.data.products);

        const orderResponse = await OrderService.GetOrdersForUser(userId);

        if(orderResponse.success && orderResponse.status === 200){
          orderStore.setOrderItems(orderResponse?.data.data.items);
        }
      }
    }
    if(authStore.isAuthenticated){
      fetchData();
    }
    fetchCategories();
  }, []);

  const fetchCategories = async () => {
    const categoriesResponse = await CategoryService.Get();
    if(categoriesResponse.success && categoriesResponse.status === 200){
      setCategories(categoriesResponse.data.categories);
    }
  }

  return <div className='page-container'>
      <NotificationsProvider>
        <BrowserRouter>
          <HeaderTop />
          <Header />
          <div className='page-body'>
            {basket.isVisibleBasket && <BasketSidebar />}
            <Routes>
              <Route path="/Error" element={<ErrorPage />} />
              <Route path="*" element={<HomePage />} />
              <Route path="Auth" element={<AuthPage />} />
              <Route path="Basket" element={<BasketPage />} />
              <Route path="Search" element={<SearchPage />} />
              <Route path="Product/:id" element={<ProductPage />} />
              <Route path="Account" element={<AccountPage />} />
              <Route path="Order" element={<OrderPage />} />
              <Route path="Admin/Categories" element={
                <ProtectedAdminRoute>
                  <AdminCategoryPage />
                </ProtectedAdminRoute>
              } />
              <Route path="Admin/Orders" element={
                <ProtectedAdminRoute>
                  <AdminOrdersPage />
                </ProtectedAdminRoute>
              } />
              <Route path="Admin/Products" element={
                <ProtectedAdminRoute>
                  <AdminProductsPage />
                </ProtectedAdminRoute>
              } />
              <Route path="Admin/News" element={
                <ProtectedAdminRoute>
                  <AdminNewsPage />
                </ProtectedAdminRoute>
              } />
              <Route path="Admin/Product/:id" element={
                <ProtectedAdminRoute>
                  <AdminProductPage />
                </ProtectedAdminRoute>
              } />
              <Route path="Admin/Users" element={
                <ProtectedAdminRoute>
                  <AdminUsersPage />
                </ProtectedAdminRoute>
              } />
            </Routes>
          </div>
          <Footer />
        </BrowserRouter>
      </NotificationsProvider>
  </div>
});

export default App;