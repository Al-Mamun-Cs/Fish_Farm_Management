import { ProjectType } from "./ProjectType";

export interface IProjectTypePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: ProjectType[];
    permission: any;
}
export class ProjectTypePagination implements IProjectTypePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: ProjectType[] = [];
    permission: any;

}
