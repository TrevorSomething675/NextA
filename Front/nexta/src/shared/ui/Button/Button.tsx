import styles from './Button.module.css'

interface Props extends React.ButtonHTMLAttributes<HTMLButtonElement> {
    className?: string;
}

export const Button:React.FC<Props> = ({className, children}) => {
    return <button className={`${styles.button} ${className || ''}`}>
        {children}
    </button>
}