import "./globals.css"
import "./colors.css"
import { BrowserRouter, Route, Routes } from 'react-router-dom'
import { NotificationsProvider } from "./sharedLegacy/components/Notifications/Notifications"
import BasketPage from "./featuresLegacy/basket/pages/BasketPage"
import SearchPage from "./featuresLegacy/search/pages/SearchPage/SearchPage"
import Footer from "./sharedLegacy/components/Footer/Footer"
import AccountPage from "./featuresLegacy/account/pages/AccountPage"
import AdminOrdersPage from "./featuresLegacy/admin/pages/AdminOrdersPage/AdminOrdersPage"
import AdminNewsPage from "./featuresLegacy/admin/pages/AdminNewsPage/AdminNewsPage"
import { ProtectedAdminRoute } from "./http/ProtectedAdminRoute"
import { ProductPage } from "./featuresLegacy/product/pages/ProductPage/ProductPage"
import { AdminProductPage } from "./featuresLegacy/admin/pages/AdminProductPage/AdminProductPage"
import { AdminProductsPage } from "./featuresLegacy/admin/pages/AdminProductsPage/AdminProductsPage"
import { BasketSidebar } from "./featuresLegacy/basket/components/BasketSidebar/BasketSidebar"
import { observer } from "mobx-react"
import { AdminCategoryPage } from "./featuresLegacy/admin/pages/AdminCategoryPage/AdminCategoryPage"
import { AdminUsersPage } from "./featuresLegacy/admin/pages/AdminUsersPage/AdminUsersPage"
import { Header } from "./widgets/ui/header/Header"
import { HeaderTop } from "./widgets/ui/header/headerTop/HeaderTop"
import { HomePage } from "./pages/home"
import { ErrorPage } from "./pages/error"
import basketStore from "./shared/stores/basket/basketStore"
import { AuthPage } from "./pages/auth"
import { OrderPage } from "./pages/order"

const App = observer(() => {
  return <div className='page-container'>
      <NotificationsProvider>
        <BrowserRouter>
          <HeaderTop />
          <Header />
          <div className='page-body'>
            {basketStore.isVisibleBasket && <BasketSidebar />}
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