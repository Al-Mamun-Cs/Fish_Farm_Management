import { Upozila } from "./Upozila";

export interface IUpozilaPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: Upozila[];
    permission: any;
}
export class UpozilaPagination implements IUpozilaPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: Upozila[] = [];
    permission: any;

}
