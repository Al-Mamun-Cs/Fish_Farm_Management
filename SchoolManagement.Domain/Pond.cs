using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public partial class Pond : BaseDomainEntity
    {
        public Pond()
        {
            FisheriesInventoryOuts = new HashSet<FisheriesInventoryOut>();
            
        }

        public int PondId { get; set; }
        public string? NameEnglish { get; set; }
        public string? NameBangla { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<FisheriesInventoryOut> FisheriesInventoryOuts { get; set; }
        
    }
}
