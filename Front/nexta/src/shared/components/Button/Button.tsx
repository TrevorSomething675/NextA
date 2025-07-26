import styles from './Button.module.css'
import React from "react"

interface Props {
    className?:string;
    content:string;
    onClick?: () => void;
    type?: 'button' | 'submit' | 'reset';
    isLoading?:boolean;
}

const Button:React.FC<Props> = ({ className, content, onClick, type='button', isLoading }) => {

    if(isLoading){
        return <button className={`${styles.button} ${className || ''}`}>
            <img src="/loading2.gif" className={styles.gif}/>
        </button>
    }

    return <button className={`${styles.button} ${className || ''}`} 
        type={type}
        onClick={onClick}>
        {content}
    </button>
}

export default Button;