import { OnlineChat } from './onlinechat';

export interface IOnlineChatPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: OnlineChat[]; 
}

export class OnlineChatPagination implements IOnlineChatPagination { 
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: OnlineChat[] = []; 
}
 