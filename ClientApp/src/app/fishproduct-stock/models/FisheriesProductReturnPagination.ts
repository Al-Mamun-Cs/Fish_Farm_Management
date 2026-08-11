import { FisheriesProductReturn } from "./FisheriesProductReturn";

export interface IFisheriesProductReturnPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: FisheriesProductReturn[];
    permission: any;
}
export class FisheriesProductReturnPagination implements IFisheriesProductReturnPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: FisheriesProductReturn[] = [];
    permission: any;

}
