using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public partial class Warehouse : BaseDomainEntity
    {
        public Warehouse()
        {
            FisheriesInventorys = new HashSet<FisheriesInventory>();
            FisheriesInventoryOuts = new HashSet<FisheriesInventoryOut>();
            ShopInventorys = new HashSet<ShopInventory>();
            ShopInventoryDetails = new HashSet<ShopInventoryDetail>();
            FisheriesProductTypes = new HashSet<FisheriesProductType>();
            DailyCostVaucherReasons = new HashSet<DailyCostVaucherReason>();
            DailyMiscellaneousCosts = new HashSet<DailyMiscellaneousCost>();
            ShopGoodSales = new HashSet<ShopGoodSale>();
            ShopGoodSaleDetails = new HashSet<ShopGoodSaleDetail>();
            ShopHandCashWithdrows = new HashSet<ShopHandCashWithdrow>();
            CompanyInvestorReturns = new HashSet<CompanyInvestorReturn>();
            FishSales = new HashSet<FishSale>();
            Depositors = new HashSet<Depositor>();
            DepositorInstallments = new HashSet<DepositorInstallment>();
            DepositorInvestments = new HashSet<DepositorInvestment>();
            InvestmentIncomes = new HashSet<InvestmentIncome>();
        }

        public int WarehouseId { get; set; }
        public string? WarehouseName { get; set; }
        public string? WarehouseAddress { get; set; }
        public string? Location { get; set; }
        public string? ContactPerson { get; set; }
        public string? ContactNo { get; set; }
        public decimal? CashAmount { get; set; }
        public decimal? BankBalance { get; set; }
        public decimal? CashInHand { get; set; }
        public string? Remarks { get; set; }
        public string? ProductImages { get; set; }
        public string? BusinessImages { get; set; }
        public string? BusinessUnitName { get; set; }
        public string? BussinessUnitDescriptions { get; set; }
        public bool? IsWebsite { get; set; }
        public bool? IsEshop { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Supplier> Suppliers { get; set; }
        public virtual ICollection<Designation> Designations { get; set; }
        public virtual ICollection<FisheriesInventory> FisheriesInventorys { get; set; }
        public virtual ICollection<FisheriesInventoryOut> FisheriesInventoryOuts { get; set; }
        public virtual ICollection<ShopInventory> ShopInventorys { get; set; }
        public virtual ICollection<ShopInventoryDetail> ShopInventoryDetails { get; set; }
        public virtual ICollection<FisheriesProductType> FisheriesProductTypes { get; set; }
        public virtual ICollection<DailyCostVaucherReason> DailyCostVaucherReasons { get; set; }
        public virtual ICollection<DailyMiscellaneousCost> DailyMiscellaneousCosts { get; set; }
        public virtual ICollection<ShopGoodSale> ShopGoodSales { get; set; }
        public virtual ICollection<ShopGoodSaleDetail> ShopGoodSaleDetails { get; set; }
        public virtual ICollection<ShopHandCashWithdrow> ShopHandCashWithdrows { get; set; }
        public virtual ICollection<CompanyInvestorReturn> CompanyInvestorReturns { get; set; }
        public virtual ICollection<FishSale> FishSales { get; set; }
        public virtual ICollection<Depositor> Depositors { get; set; }
        public virtual ICollection<DepositorInstallment> DepositorInstallments { get; set; }
        public virtual ICollection<DepositorInvestment> DepositorInvestments { get; set; }
        public virtual ICollection<InvestmentIncome> InvestmentIncomes { get; set; }
    }
}
