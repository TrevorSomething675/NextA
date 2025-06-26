import { Detail } from "../../../shared/entities/Detail";
import { PagedData } from "../../../shared/models/PagedDataT";
import { GetDetailsFilter } from "./GetDetails";

export interface GetWarehouseRequest{
    filter:GetDetailsFilter
}

export interface GetWarehouseResponse{
    details: PagedData<Detail>
}
