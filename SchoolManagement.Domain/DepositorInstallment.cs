using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public partial class DepositorInstallment : BaseDomainEntity
    {
        public DepositorInstallment()
        {
            //FisheriesInventoryDetails = new HashSet<FisheriesInventoryDetail>();
            
        }

        public int DepositorInstallmentId { get; set; }
        public int? WarehouseId { get; set; }
        public int? DepositorId { get; set; }
        public DateTime? InstallmentDate { get; set; }
        public decimal? InstallmentAmount { get; set; }
        public byte? Month { get; set; }
        public short? Year { get; set; }
        public string? Image { get; set; }
        public int? ApproveStatus { get; set; }
        public string? ApproveBy { get; set; }
        public DateTime? ApproveDate { get; set; }
        public bool IsActive { get; set; }

        public virtual Warehouse? Warehouse { get; set; }
        public virtual Depositor? Depositor { get; set; }

        //public virtual ICollection<FisheriesInventoryDetail> FisheriesInventoryDetails { get; set; }

    }
}
