export interface ShopHandCashWithdrow {
    shopHandCashWithdrowId: number,
    warehouseId: number,
    presentAmount: string,
    transferAmount:string,
    remainingAmount: string,
    transferDate: string,
    transferReason: string,
    approveStatus: string,
    approveBy: string,
    approvedBy: string,
    approveDate: string,
    isActive: boolean
}