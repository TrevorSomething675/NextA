interface GetAllOrdersFilter{
    pageNumber?:number,
    pageSize?:number,
    statuses?:number[];
    searchTerm:string;
}

export default GetAllOrdersFilter;