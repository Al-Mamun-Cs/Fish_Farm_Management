export interface DepositorInstallment {
    depositorInstallmentId: number,
    warehouseId: number,
    depositorId: number,
    installmentDate: string,
    installmentAmount: string,
    image: string,
    approveStatus: number,
    approveBy: string,
    approveDate: string,
    isActive: boolean
}