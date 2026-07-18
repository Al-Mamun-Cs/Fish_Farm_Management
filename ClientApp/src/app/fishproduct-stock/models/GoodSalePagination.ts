import { GoodSale } from "./GoodSale";

export interface IGoodSalePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: GoodSale[];
}
export class GoodSalePagination implements IGoodSalePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: GoodSale[] = [];


}
