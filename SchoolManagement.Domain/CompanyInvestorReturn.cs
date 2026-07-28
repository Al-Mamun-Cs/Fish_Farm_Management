using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public partial class CompanyInvestorReturn : BaseDomainEntity
    {
        public CompanyInvestorReturn()
        {
            //FisheriesInventoryDetails = new HashSet<FisheriesInventoryDetail>();
            
        }

        public int CompanyInvestorReturnId { get; set; }
        public int? WarehouseId { get; set; }
        public int? CompanyInvestorId { get; set; }
        public int? PaymentStatusId { get; set; }
        public int? Type { get; set; }
        public DateTime? Date { get; set; }
        public decimal? Amount { get; set; }
        public string? Remarks  { get; set; }
        public int? ApproveStatus { get; set; }
        public string? ApproveBy { get; set; }
        public DateTime? ApproveDate { get; set; }
        public bool IsActive { get; set; }

        public virtual Warehouse? Warehouse { get; set; }
        public virtual CompanyInvestor? CompanyInvestor { get; set; }
        public virtual PaymentStatus? PaymentStatus { get; set; }

        //public virtual ICollection<FisheriesInventoryDetail> FisheriesInventoryDetails { get; set; }

    }
}
