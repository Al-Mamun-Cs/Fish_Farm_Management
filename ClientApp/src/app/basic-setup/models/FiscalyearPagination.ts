import { Fiscalyear } from "./Fiscalyear";

export interface IFiscalyearPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: Fiscalyear[];
    permission: any;
}
export class FiscalyearPagination implements IFiscalyearPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: Fiscalyear[] = [];
    permission: any;

}
