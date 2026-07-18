using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public partial class Religion : BaseDomainEntity
    {
        public Religion()
        {
            //Empolyees = new HashSet<Empolyee>();
            
        }

        public int ReligionId { get; set; }
        public string? FullName { get; set; }
        public string? ShortName { get; set; }
        public bool IsActive { get; set; }

        //public virtual ICollection<Empolyee> Empolyees { get; set; }
        
    }
}
