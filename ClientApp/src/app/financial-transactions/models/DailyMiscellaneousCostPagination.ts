import { DailyMiscellaneousCost } from "./DailyMiscellaneousCost";

export interface IDailyMiscellaneousCostPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: DailyMiscellaneousCost[];
    permission: any;
}
export class DailyMiscellaneousCostPagination implements IDailyMiscellaneousCostPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: DailyMiscellaneousCost[] = [];
    permission: any;

}
