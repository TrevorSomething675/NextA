export interface AddAdminImageRequest{
    name:string,
    bucket:string,
    base64string:string
}

export interface AddAdminImageResponse{
    imageId:string
}