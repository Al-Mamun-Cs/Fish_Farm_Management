using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public partial class Country : BaseDomainEntity
    {
        public Country()
        {
            Suppliers = new HashSet<Supplier>();
            
        }

        public int CountryId { get; set; }
        public string? Name { get; set; }
        public int? Status { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Supplier> Suppliers { get; set; }
        
    }
}
