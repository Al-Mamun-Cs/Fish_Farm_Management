export interface CompanyInvestorReturn {
    companyInvestorReturnId: number,
    warehouseId: number,
    companyInvestorId:number,
    paymentStatusId:number,
    type: number,
    date:string,
    amount: string,
    remarks: string,
    approveStatus: string,
    approveBy: string,
    approveDate: string,
    isActive: boolean
}