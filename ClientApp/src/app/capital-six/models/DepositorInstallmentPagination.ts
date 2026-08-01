import { DepositorInstallment } from "./DepositorInstallment";

export interface IDepositorInstallmentPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: DepositorInstallment[];
    permission: any;
}
export class DepositorInstallmentPagination implements IDepositorInstallmentPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: DepositorInstallment[] = [];
    permission: any;

}
