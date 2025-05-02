import Footer from "@/components/footer/footer";
import Header from "@/components/header/header";
import Home from "@/components/home/home";

const HomePage = () => {
  return <div className='page-container'>
    <Header />
    <div className='page-body'>
      <Home />
    </div>
    <Footer />
  </div>
}

export default HomePage;