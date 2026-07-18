import { Pond } from "./Pond";

export interface IPondPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: Pond[];
    permission: any;
}
export class PondPagination implements IPondPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: Pond[] = [];
    permission: any;

}
