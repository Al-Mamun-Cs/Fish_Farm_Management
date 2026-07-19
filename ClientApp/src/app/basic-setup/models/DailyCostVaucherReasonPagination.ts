import { DailyCostVaucherReason } from "./DailyCostVaucherReason";

export interface IDailyCostVaucherReasonPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: DailyCostVaucherReason[];
    permission: any;
}
export class DailyCostVaucherReasonPagination implements IDailyCostVaucherReasonPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: DailyCostVaucherReason[] = [];
    permission: any;

}
