export interface InvestmentIncome {
    investmentIncomeId: number,
    warehouseId: number,
    depositorInvestmentId: number,
    type: number,
    date: string,
    amount: string,
    approveStatus: number,
    approveBy: string,
    approveDate: string,
    isActive: boolean
}