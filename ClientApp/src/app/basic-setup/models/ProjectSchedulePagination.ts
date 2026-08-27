import { ProjectSchedule } from "./ProjectSchedule";

export interface IProjectSchedulePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: ProjectSchedule[];
    permission: any;
}
export class ProjectSchedulePagination implements IProjectSchedulePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: ProjectSchedule[] = [];
    permission: any;

}
