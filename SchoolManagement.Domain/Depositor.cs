using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public partial class Depositor : BaseDomainEntity
    {
        public Depositor()
        {
            DepositorInstallments = new HashSet<DepositorInstallment>();
            DepositorInvestments = new HashSet<DepositorInvestment>();
            
        }

        public int DepositorId { get; set; }
        public int? WarehouseId { get; set; }
        public string? DepositorName { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public decimal? PresentBalance { get; set; }
        public bool IsActive { get; set; }

        public virtual Warehouse? Warehouse { get; set; }

        public virtual ICollection<DepositorInstallment> DepositorInstallments { get; set; }
        public virtual ICollection<DepositorInvestment> DepositorInvestments { get; set; }

    }
}
