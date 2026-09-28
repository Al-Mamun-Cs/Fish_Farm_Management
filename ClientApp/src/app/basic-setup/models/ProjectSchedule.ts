export interface ProjectSchedule {
    projectScheduleId: number,
    warehouseId: number,
    pondId: number,
    projectTypeId:number,
    projectType:string,
    name: string,
    dateFrom: string,
    dateTo: string,
    activeStatus: number,
    isActive: boolean
}