import "./globals.css"
import "./colors.css"
import { BrowserRouter, Route, Routes } from 'react-router-dom'
import { NotificationsProvider } from "./shared/contexts/notifications/NotificationsContext"
import AdminOrdersPage from "./featuresLegacy/admin/pages/AdminOrdersPage/AdminOrdersPage"
import AdminNewsPage from "./featuresLegacy/admin/pages/AdminNewsPage/AdminNewsPage"
import { ProtectedAdminRoute } from "./http/ProtectedAdminRoute"
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
import { BasketPage } from "./pages/basket"
import { SearchPage } from "./pages/search"
import { Footer } from "./widgets/ui/footer/Footer"
import { useEffect } from "react"
import authStore from "./shared/stores/auth/authStore"
import { BasketApi } from "./shared/http/basket/basketApi"
import { useGetCategories } from "./features/category/getAdminCategories/useGetAdminCategories"
import { ProductPage } from "./pages/product"

const App = observer(() => {
  const { getCategories } = useGetCategories();
  
  useEffect(() => {
    fetchData();
  }, []);

  const fetchData = async() => {
    await getCategories();
    if(authStore.isAuthenticated){
      const basketResponse = await BasketApi.GetByUserId(authStore.user.id!);
      if(basketResponse.success && basketResponse.status === 200){
        basketStore.setBasketItems(basketResponse.data.products);
      }
    }
  }

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