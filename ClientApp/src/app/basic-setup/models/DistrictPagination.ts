import { District } from "./District";

export interface IDistrictPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: District[];
    permission: any;
}
export class DistrictPagination implements IDistrictPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: District[] = [];
    permission: any;

}
