export interface DailyMiscellaneousCost {
    dailyMiscellaneousCostId: number,
    warehouseId: number,
    dailyCostVaucherReasonId: number,
    empolyeeId:number,
    paymentStatusId: number,
    transactionType:number,
    supplierId:number,
    transactionDate: string,
    amount: string,
    remarks: string,
    approvedStatus: string,
    approvedBy: string,
    approvedDate: string,
    isActive: boolean
}