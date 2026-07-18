import { FisheriesInventoryOut } from "./FisheriesInventoryOut";

export interface IFisheriesInventoryOutPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: FisheriesInventoryOut[];
    permission: any;
}
export class FisheriesInventoryOutPagination implements IFisheriesInventoryOutPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: FisheriesInventoryOut[] = [];
    permission: any;

}
