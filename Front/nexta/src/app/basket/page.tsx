import Basket from "@/components/basket/basket";
import Footer from "@/components/footer/footer";
import Header from "@/components/header/header";

const BacketPage = () => {
    return <div className='page-container'>
        <Header />
        <div className='page-body'>
            <Basket />
        </div>
        <Footer />
    </div>
}

export default BacketPage;