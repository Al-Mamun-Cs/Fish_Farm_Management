import { ShopInventory } from "./ShopInventory";

export interface IShopInventoryPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: ShopInventory[];
    permission: any;
}
export class ShopInventoryPagination implements IShopInventoryPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: ShopInventory[] = [];
    permission: any;

}
