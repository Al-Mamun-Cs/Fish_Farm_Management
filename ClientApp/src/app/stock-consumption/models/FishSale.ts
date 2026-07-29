export interface FishSale {
    fishSaleId: number,
    warehouseId: number,
    pondId: number,
    supplierId:number,
    fisheriesUnitId: number,
    paymentStatusId: number,
    saleDate: string,
    saleQty: string,
    unitSalePrice: string,
    totalSalePrice: string,
    salePaidAmount: string,
    saleDueAmount: string,
    isActive: boolean
}