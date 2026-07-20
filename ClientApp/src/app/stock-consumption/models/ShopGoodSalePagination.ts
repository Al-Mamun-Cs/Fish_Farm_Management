import { ShopGoodSale } from "./ShopGoodSale";

export interface IShopGoodSalePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: ShopGoodSale[];
    permission: any;
}
export class ShopGoodSalePagination implements IShopGoodSalePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: ShopGoodSale[] = [];
    permission: any;

}
