import { FisheriesUnit } from "./FisheriesUnit";

export interface IFisheriesUnitPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: FisheriesUnit[];
    permission: any;
}
export class FisheriesUnitPagination implements IFisheriesUnitPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: FisheriesUnit[] = [];
    permission: any;

}
