using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public partial class InvestmentIncome : BaseDomainEntity
    {
        public InvestmentIncome()
        {
            //FisheriesInventoryDetails = new HashSet<FisheriesInventoryDetail>();
            
        }

        public int InvestmentIncomeId { get; set; }
        public int? WarehouseId { get; set; }
        public int? DepositorInvestmentId { get; set; }
        public int? Type { get; set; }
        public DateTime? Date { get; set; }
        public decimal? Amount { get; set; }
        public int? ApproveStatus { get; set; }
        public string? ApproveBy { get; set; }
        public DateTime? ApproveDate { get; set; }
        public bool IsActive { get; set; }

        public virtual Warehouse? Warehouse { get; set; }
        public virtual DepositorInvestment? DepositorInvestment { get; set; }

        //public virtual ICollection<FisheriesInventoryDetail> FisheriesInventoryDetails { get; set; }

    }
}
