using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public partial class ProjectSchedule : BaseDomainEntity
    {
        public ProjectSchedule()
        {
            FisheriesInventoryOuts = new HashSet<FisheriesInventoryOut>();

        }

        public int ProjectScheduleId { get; set; }
        public int? WarehouseId { get; set; }
        public int? PondId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int? ActiveStatus { get; set; }
        public bool IsActive { get; set; }

        public virtual Warehouse? Warehouse { get; set; }
        public virtual Pond? Pond { get; set; }

        public virtual ICollection<FisheriesInventoryOut> FisheriesInventoryOuts { get; set; }

    }
}
