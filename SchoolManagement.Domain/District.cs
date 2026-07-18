using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public partial class District : BaseDomainEntity
    {
        public District()
        {
            Upozilas = new HashSet<Upozila>();
            Suppliers = new HashSet<Supplier>();
            
        }

        public int DistrictId { get; set; }
        public int DivisionId { get; set; }
        public string DistrictName { get; set; }
        public string? DistrictNameBangla { get; set; }
        public int? Status { get; set; }
        public int? ManuPositon { get; set; }
        public decimal? ShippingCharge { get; set; }
        public bool IsActive { get; set; }

        public virtual Division? Division { get; set; } 
        public virtual ICollection<Upozila> Upozilas { get; set; }
        public virtual ICollection<Supplier> Suppliers { get; set; }

    }
}
