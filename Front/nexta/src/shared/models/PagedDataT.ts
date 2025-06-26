export interface PagedData<T>{
    items: T[],
    count:number,
    pageCount:number
}