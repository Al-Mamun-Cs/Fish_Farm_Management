export interface CompanyInvestor {
    companyInvestorId: number,
    warehouseId: number,
    fullName:string,
    shortName:string,
    phoneNo: string,
    email:string,
    address: string,
    date: string,
    investAmount: string,
    returnInvestAmount: string,
    profitAmount: string,
    isActive: boolean
}