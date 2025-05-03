import Header from './components/header/header'
import Footer from './components/footer/footer'
import "./globals.css"
import "./colors.css"
import { BrowserRouter, Route, Routes } from 'react-router-dom'
import AuthPage from './pages/auth/AuthPage'
import AccountPage from './pages/account/AccountPage'
import BasketPage from './pages/basket/BasketPage'
import HomePage from './pages/home/HomePage'

const App = () => {
  return <div className='page-container'>
      <BrowserRouter>
        <Header />
        <div className='page-body'>
          <Routes>
            <Route path="*" element={<HomePage />} />
            <Route path="Auth" element={<AuthPage />} />
            <Route path="Account" element={<AccountPage />} />
            <Route path="Basket" element={<BasketPage />} />
          </Routes>
        </div>
        <Footer />
      </BrowserRouter>
  </div>
}

export default App