using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public partial class Designation : BaseDomainEntity
    {
        public Designation()
        {
            //Empolyees = new HashSet<Empolyee>();
            
        }

        public int DesignationId { get; set; }
        public int? WarehouseId { get; set; }
        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public int? MenuPosition { get; set; }
        public int? ServiceAge { get; set; }
        public string? Remarks { get; set; }
        public bool IsActive { get; set; }

        public virtual Warehouse? Warehouse { get; set; }

        //public virtual ICollection<Empolyee> Empolyees { get; set; }
        
    }
}
