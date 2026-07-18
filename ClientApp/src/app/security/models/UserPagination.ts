import {User} from './User';

export interface IUserPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: User[];
    permission: any;
}
 
export class UserPagination implements IUserPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: User[] = [];
    permission: any;

}
