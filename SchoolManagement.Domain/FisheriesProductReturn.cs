using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public partial class FisheriesProductReturn : BaseDomainEntity
    {
        public FisheriesProductReturn()
        {
            //InvestmentIncomes = new HashSet<InvestmentIncome>();

        }

        public int FisheriesProductReturnId { get; set; }
        public int? WarehouseId { get; set; }
        public int? SupplierId { get; set; }
        public int? FisheriesProductTypeId { get; set; }
        public int? FisheriesInventoryDetailId { get; set; }
        public int? FisheriesInventoryId { get; set; }
        public int? PaymentReturnType { get; set; }
        public DateTime? Date { get; set; }
        public decimal? ReturnQty { get; set; }
        public decimal? ReturnAmount { get; set; }
        public decimal? ActualReturnValue { get; set; }
        public decimal? DepreciationValue { get; set; }
        public string? Remarks { get; set; }
        public int? ApproveStatus { get; set; }
        public string? ApproveBy { get; set; }
        public DateTime? ApproveDate { get; set; }
        public bool IsActive { get; set; }

        public virtual Warehouse? Warehouse { get; set; }
        public virtual Supplier? Supplier { get; set; }
        public virtual FisheriesProductType? FisheriesProductType { get; set; }
        public virtual FisheriesInventory? FisheriesInventory { get; set; }
        public virtual FisheriesInventoryDetail? FisheriesInventoryDetail { get; set; }

        //public virtual ICollection<InvestmentIncome> InvestmentIncomes { get; set; }

    }
    
}
