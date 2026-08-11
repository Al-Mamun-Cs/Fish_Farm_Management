export interface FisheriesProductReturn {    
    fisheriesProductReturnId: number,
    warehouseId: number,
    supplierId:number,
    fisheriesProductTypeId:number,
    fisheriesInventoryDetailId:number,
    fisheriesInventoryId:number,
    paymentReturnType:number,
    date:string,
    returnQty: string,
    returnAmount:string,
    actualReturnValue:string,
    depreciationValue:string,
    remarks: string,
    approveStatus:string,
    approveBy:string,
    approveDate:string,
    isActive: boolean
}
