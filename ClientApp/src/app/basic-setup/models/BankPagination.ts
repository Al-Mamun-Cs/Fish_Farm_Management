import { Bank } from "./Bank";

export interface IBankPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: Bank[];
    permission: any;
}
export class BankPagination implements IBankPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: Bank[] = [];
    permission: any;

}
