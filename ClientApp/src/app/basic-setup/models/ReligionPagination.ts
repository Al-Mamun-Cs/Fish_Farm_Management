import { Religion } from "./Religion";

export interface IReligionPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: Religion[];
    permission: any;
}
export class ReligionPagination implements IReligionPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: Religion[] = [];
    permission: any;

}
