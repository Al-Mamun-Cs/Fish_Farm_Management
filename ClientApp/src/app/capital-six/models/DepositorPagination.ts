import { Depositor } from "./Depositor";

export interface IDepositorPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: Depositor[];
    permission: any;
}
export class DepositorPagination implements IDepositorPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: Depositor[] = [];
    permission: any;

}
