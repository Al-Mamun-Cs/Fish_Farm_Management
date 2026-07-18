import { FisheriesProductType } from "./FisheriesProductType";

export interface IFisheriesProductTypePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: FisheriesProductType[];
    permission: any;
}
export class FisheriesProductTypePagination implements IFisheriesProductTypePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: FisheriesProductType[] = [];
    permission: any;

}
