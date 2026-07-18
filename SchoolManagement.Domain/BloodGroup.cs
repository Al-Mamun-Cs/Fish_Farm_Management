using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public partial class BloodGroup : BaseDomainEntity
    {
        public BloodGroup()
        {
            //Empolyees = new HashSet<Empolyee>();
            
        }

        public int BloodGroupId { get; set; }
        public string? FullName { get; set; }
        public string? ShortName { get; set; }
        public bool IsActive { get; set; }

        //public virtual ICollection<Empolyee> Empolyees { get; set; }
        
    }
}
