import "./globals.css"
import "./colors.css"
import { BrowserRouter, Route, Routes } from 'react-router-dom'
import { useEffect } from 'react'
import OrderService from './services/OrderService'
import orderStore from './stores/orderStore'
import { NotificationsProvider } from "./sharedLegacy/components/Notifications/Notifications"
import HomePage from "./featuresLegacy/home/pages/HomePage"
import BasketPage from "./featuresLegacy/basket/pages/BasketPage"
import SearchPage from "./featuresLegacy/search/pages/SearchPage/SearchPage"
import OrderPage from "./featuresLegacy/order/pages/OrdersPage"
import BasketService from "./services/BasketService"
import {Header} from "./sharedLegacy/components/Header/Header"
import Footer from "./sharedLegacy/components/Footer/Footer"
import AccountPage from "./featuresLegacy/account/pages/AccountPage"
import AdminOrdersPage from "./featuresLegacy/admin/pages/AdminOrdersPage/AdminOrdersPage"
import AdminNewsPage from "./featuresLegacy/admin/pages/AdminNewsPage/AdminNewsPage"
import { ErrorPage } from "./featuresLegacy/error/pages/ErrorPage"
import { ProtectedAdminRoute } from "./http/ProtectedAdminRoute"
import { ProductPage } from "./featuresLegacy/product/pages/ProductPage/ProductPage"
import { AuthPage } from "./featuresLegacy/auth/pages/AuthPage"
import { AuthService } from "./services/AuthService"
import authStore from "./stores/AuthStore/authStore"
import basket from "./stores/basket"
import { AdminProductPage } from "./featuresLegacy/admin/pages/AdminProductPage/AdminProductPage"
import { AdminProductsPage } from "./featuresLegacy/admin/pages/AdminProductsPage/AdminProductsPage"
import { BasketSidebar } from "./featuresLegacy/basket/components/BasketSidebar/BasketSidebar"
import { observer } from "mobx-react"
import { AdminCategoryPage } from "./featuresLegacy/admin/pages/AdminCategoryPage/AdminCategoryPage"
import CategoryService from "./services/CategoryService"
import { useCategoriesStore } from "./stores/categoriesStore"
import { AdminUsersPage } from "./featuresLegacy/admin/pages/AdminUsersPage/AdminUsersPage"
import { HeaderTop } from "./sharedLegacy/components/Header/HeaderTop/HeaderTop"

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