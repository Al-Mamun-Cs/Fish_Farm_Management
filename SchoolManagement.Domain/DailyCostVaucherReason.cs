using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public partial class DailyCostVaucherReason : BaseDomainEntity
    {
        public DailyCostVaucherReason()
        {
            DailyMiscellaneousCosts = new HashSet<DailyMiscellaneousCost>();
            
        }

        public int DailyCostVaucherReasonId { get; set; }
        public int? WarehouseId { get; set; }
        public string? FullName { get; set; }
        public string? ShortName { get; set; }
        public int? MenuPosition { get; set; }
        public int? TransactionType { get; set; }
        public bool IsActive { get; set; }

        public virtual Warehouse? Warehouse { get; set; }

        public virtual ICollection<DailyMiscellaneousCost> DailyMiscellaneousCosts { get; set; }
        
    }
}
