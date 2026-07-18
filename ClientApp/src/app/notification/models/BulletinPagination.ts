import { Bulletin } from "./Bulletin";

export interface IBulletinPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: Bulletin[];
    permission: any;
}
export class BulletinPagination implements IBulletinPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: Bulletin[] = [];

    permission: any;
}
