import { CompanyInvestorReturn } from "./CompanyInvestorReturn";

export interface ICompanyInvestorReturnPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: CompanyInvestorReturn[];
    permission: any;
}
export class CompanyInvestorReturnPagination implements ICompanyInvestorReturnPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: CompanyInvestorReturn[] = [];
    permission: any;

}
