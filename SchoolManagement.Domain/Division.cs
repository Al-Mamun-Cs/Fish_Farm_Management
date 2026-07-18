using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public partial class Division : BaseDomainEntity
    {
        public Division()
        {
            Districts = new HashSet<District>();
            Suppliers = new HashSet<Supplier>();
            
        }

        public int DivisionId { get; set; }
        public string DivisionName { get; set; }
        public string? NameBangla { get; set; }
        public int? Status { get; set; }
        public int? ManuPositon { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<District> Districts { get; set; }
        public virtual ICollection<Supplier> Suppliers { get; set; }
        
    }
}
