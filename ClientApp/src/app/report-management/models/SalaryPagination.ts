import { Salary } from "./Salary";

export interface ISalaryPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: Salary[];
    permission: any;
}
export class SalaryPagination implements ISalaryPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: Salary[] = [];
    permission: any;

}
