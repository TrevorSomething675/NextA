import styles from './image.module.css';

interface Props{
    isBase64Image?:boolean,
    srcImage:string,
    base64String?:string,
    className?:string
}

const Image:React.FC<Props> = ({isBase64Image, srcImage, base64String, className}) =>{
    const combinedClassName = `${styles.image} ${className || ''}`.trim();

    {if(isBase64Image){
        return <img src={`data:image/jpeg;base64,${base64String}`} className={combinedClassName}/>
    } else {
        return <img src={srcImage} className={combinedClassName}/>
    }}
}

export default Image;