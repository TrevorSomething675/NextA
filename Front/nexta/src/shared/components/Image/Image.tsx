import styles from './Image.module.css';

interface Props{
    isBase64Image?:boolean,
    srcImage?:string,
    base64String?:string,
    className?:string,
    isLoading?:boolean
};

const Image: React.FC<Props> = ({ isBase64Image, srcImage, base64String, className, isLoading }) => {
    const combinedClassName = `${styles.image} ${className || ''}`.trim();

    if (isLoading) {
        return <img src="/loading.gif" className={styles.image} />;
    }

    if (isBase64Image) {
        return base64String ? (
            <img src={`data:image/jpeg;base64,${base64String}`} className={combinedClassName} />
        ) : (
            <img src="/defaultImage.jpg" className={combinedClassName} />
        );
    }

    return <img src={srcImage} className={combinedClassName} />;
};

export default Image;