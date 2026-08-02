import { DepositorInvestment } from "./DepositorInvestment";

export interface IDepositorInvestmentPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: DepositorInvestment[];
    permission: any;
}
export class DepositorInvestmentPagination implements IDepositorInvestmentPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: DepositorInvestment[] = [];
    permission: any;

}
