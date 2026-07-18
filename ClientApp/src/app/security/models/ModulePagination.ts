import {Module} from "./Module"; 
export interface IModulePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: Module[];
    permission: any;
}

export class ModulePagination implements IModulePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: Module[] = [];
    permission: any;
}