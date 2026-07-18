import { EasyBikeFiscalYear } from "./EasyBikeFiscalYear";

export interface IEasyBikeFiscalYearPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: EasyBikeFiscalYear[];
}
export class EasyBikeFiscalYearPagination implements IEasyBikeFiscalYearPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: EasyBikeFiscalYear[] = [];


}
