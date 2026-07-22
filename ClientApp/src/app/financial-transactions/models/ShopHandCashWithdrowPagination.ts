import { ShopHandCashWithdrow } from "./ShopHandCashWithdrow";

export interface IShopHandCashWithdrowPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: ShopHandCashWithdrow[];
    permission: any;
}
export class ShopHandCashWithdrowPagination implements IShopHandCashWithdrowPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: ShopHandCashWithdrow[] = [];
    permission: any;

}
