export interface ProjectSchedule {
    projectScheduleId: number,
    warehouseId: number,
    pondId: number,
    dateFrom: string,
    dateTo: string,
    activeStatus: number,
    isActive: boolean
}