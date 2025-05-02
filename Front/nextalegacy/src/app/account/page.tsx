import Account from "@/components/account/account";
import Header from "@/components/header/header";
import Footer from "@/components/footer/footer";

const AccountPage = () => {
    return <div className='page-container'>
        <Header />
        <div className='page-body'>
            <Account />
        </div>
        <Footer />
    </div>
}

export default AccountPage;