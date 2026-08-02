import { InvestmentIncome } from "./InvestmentIncome";

export interface IInvestmentIncomePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: InvestmentIncome[];
    permission: any;
}
export class InvestmentIncomePagination implements IInvestmentIncomePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: InvestmentIncome[] = [];
    permission: any;

}
