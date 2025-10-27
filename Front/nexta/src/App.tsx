import "./globals.css"
import "./colors.css"
import { BrowserRouter, Route, Routes } from 'react-router-dom'
import { NotificationsProvider } from "./sharedLegacy/components/Notifications/Notifications"
import SearchPage from "./featuresLegacy/search/pages/SearchPage/SearchPage"
import Footer from "./sharedLegacy/components/Footer/Footer"
import AdminOrdersPage from "./featuresLegacy/admin/pages/AdminOrdersPage/AdminOrdersPage"
import AdminNewsPage from "./featuresLegacy/admin/pages/AdminNewsPage/AdminNewsPage"
import { ProtectedAdminRoute } from "./http/ProtectedAdminRoute"
import { ProductPage } from "./pages/product/ui/ProductPage"
import { AdminProductPage } from "./featuresLegacy/admin/pages/AdminProductPage/AdminProductPage"
import { AdminProductsPage } from "./featuresLegacy/admin/pages/AdminProductsPage/AdminProductsPage"
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
import { AccountPage } from "./pages/account"
import { BasketSidebar } from "./widgets/ui/basket/basketSidebar/ui/BasketSidebar"
import BasketPage from "./pages/basket/ui/BasketPage"

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

            </Routes>
          </div>
          <Footer />
        </BrowserRouter>
      </NotificationsProvider>
  </div>
});

export default App;