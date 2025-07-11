import styles from './AvitoSvg.module.css';

const AvitoSvg = () => {
    return <svg viewBox="0 0 90 90" xmlns="http://www.w3.org/2000/svg" className={styles.svg}>
        <g fill="none">
            <path fill="#FFF" d="M0 0h90v90H0z"/>
            <circle fill="#97CF26" cx="59" cy="59" r="16"/><circle fill="#0AF" cx="29" cy="29" r="13"/><circle fill="#FF6163" cx="59" cy="29" r="10"/>
            <circle fill="#A169F7" cx="28.5" cy="58.5" r="7.5"/>
        </g>
    </svg>
}

export default AvitoSvg;