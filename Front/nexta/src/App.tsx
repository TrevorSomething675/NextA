import Header from './components/header/header'
import Home from './components/home/home'
import Footer from './components/footer/footer'
import "./globals.css"
import "./colors.css"
import { BrowserRouter, Route, Routes } from 'react-router-dom'
import AuthPage from './pages/auth/AuthPage'

const App = () => {
  return <div className='page-container'>
      <BrowserRouter>
        <Header />
        <div className='page-body'>
          <Routes>
            <Route path="*" element={<Home />} />
            <Route path="Auth" element={<AuthPage />} />
          </Routes>
        </div>
        <Footer />
      </BrowserRouter>
  </div>
}

export default App