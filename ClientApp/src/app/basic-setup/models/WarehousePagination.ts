import { Warehouse } from "./Warehouse";

export interface IWarehousePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: Warehouse[];
    permission: any;
}
export class WarehousePagination implements IWarehousePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: Warehouse[] = [];
    permission: any;

}
