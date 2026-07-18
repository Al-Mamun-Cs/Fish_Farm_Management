import {BaseSchoolName} from './BaseSchoolName';

export interface IBaseSchoolNamePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: BaseSchoolName[];
    permission: any;
}

export class BaseSchoolNamePagination implements IBaseSchoolNamePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: BaseSchoolName[] = [];
    permission: any;

}
