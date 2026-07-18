using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public partial class SupplierType : BaseDomainEntity
    {
        public SupplierType()
        {
            Suppliers = new HashSet<Supplier>();
            
        }

        public int SupplierTypeId { get; set; }
        public string? SupplierTypeName { get; set; }
        public string? Remarks { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Supplier> Suppliers { get; set; }
        
    }
}
