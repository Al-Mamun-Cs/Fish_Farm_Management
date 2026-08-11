using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public partial class DepositorInvestment : BaseDomainEntity
    {
        public DepositorInvestment()
        {
            InvestmentIncomes = new HashSet<InvestmentIncome>();

        }

        public int DepositorInvestmentId { get; set; }
        public int? WarehouseId { get; set; }
        public int? DepositorId { get; set; }
        public DateTime? InvestmenDate { get; set; }
        public decimal? InvestmenAmount { get; set; }
        public decimal? PrincipalReturn { get; set; }
        public decimal? Profit { get; set; }
        public string? BusinessOperatorName { get; set; }
        public string? Mobile { get; set; }
        public string? Address { get; set; }
        public string? Remarks { get; set; }
        public int? CloseStatus { get; set; }
        public int? ApproveStatus { get; set; }
        public string? ApproveBy { get; set; }
        public DateTime? ApproveDate { get; set; }
        public bool IsActive { get; set; }

        public virtual Warehouse? Warehouse { get; set; }
        public virtual Depositor? Depositor { get; set; }

        public virtual ICollection<InvestmentIncome> InvestmentIncomes { get; set; }

    }
}
