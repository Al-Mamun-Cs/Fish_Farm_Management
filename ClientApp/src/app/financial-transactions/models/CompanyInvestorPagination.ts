import { CompanyInvestor } from "./CompanyInvestor";

export interface ICompanyInvestorPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: CompanyInvestor[];
    permission: any;
}
export class CompanyInvestorPagination implements ICompanyInvestorPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: CompanyInvestor[] = [];
    permission: any;

}
