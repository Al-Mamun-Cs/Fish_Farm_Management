using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public partial class FisheriesProductType : BaseDomainEntity
    {
        public FisheriesProductType()
        {
            FisheriesInventoryDetails = new HashSet<FisheriesInventoryDetail>();
            FisheriesInventoryOuts = new HashSet<FisheriesInventoryOut>();
            
        }

        public int FisheriesProductTypeId { get; set; }
        public string? NameEnglish { get; set; }
        public string? NameBangla { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<FisheriesInventoryDetail> FisheriesInventoryDetails { get; set; }
        public virtual ICollection<FisheriesInventoryOut> FisheriesInventoryOuts { get; set; }
        
    }
}
