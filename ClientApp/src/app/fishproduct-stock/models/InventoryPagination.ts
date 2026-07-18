import { Inventory } from "./Inventory";

export interface IInventoryPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: Inventory[];
    permission: any;
}
export class InventoryPagination implements IInventoryPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: Inventory[] = [];
    permission: any;

}
