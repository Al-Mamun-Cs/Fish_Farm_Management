import { FishSale } from "./FishSale";

export interface IFishSalePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: FishSale[];
    permission: any;
}
export class FishSalePagination implements IFishSalePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: FishSale[] = [];
    permission: any;

}
