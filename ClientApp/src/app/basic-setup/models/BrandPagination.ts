import { Brand } from "./Brand";

export interface IBrandPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: Brand[];
    permission: any;
}
export class BrandPagination implements IBrandPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: Brand[] = [];
    permission: any;

}
