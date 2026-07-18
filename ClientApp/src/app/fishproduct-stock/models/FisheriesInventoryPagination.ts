import { FisheriesInventory } from "./FisheriesInventory";

export interface IFisheriesInventoryPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: FisheriesInventory[];
    permission: any;
}
export class FisheriesInventoryPagination implements IFisheriesInventoryPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: FisheriesInventory[] = [];
    permission: any;

}
