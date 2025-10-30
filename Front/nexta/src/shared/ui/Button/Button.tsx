import styles from './Button.module.css'

interface Props extends React.ButtonHTMLAttributes<HTMLButtonElement> {
    className?: string;
}

export const Button:React.FC<Props> = ({className, children, ...rest}) => {
    return <button 
        {...rest}
        className={`${styles.button} ${className || ''}`}>
        {children}
    </button>
}