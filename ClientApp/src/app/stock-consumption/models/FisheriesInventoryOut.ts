export interface FisheriesInventoryOut {
    fisheriesInventoryOutId: number,
    warehouseId: number,
    pondId: number,
    projectScheduleId:number,
    fisheriesProductTypeId:number,
    projectTypeId:number,
    fisheriesInventoryDetailId: number,
    date: string,
    useTime:number,
    useQty: string,
    unitPurchasePrice: string,
    approveStatus: boolean,
    isActive: boolean
}