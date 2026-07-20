using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public partial class FisheriesUnit : BaseDomainEntity
    {
        public FisheriesUnit()
        {
            FisheriesInventoryDetails = new HashSet<FisheriesInventoryDetail>();
            ShopInventoryDetails = new HashSet<ShopInventoryDetail>();
            ShopGoodSaleDetails = new HashSet<ShopGoodSaleDetail>();
            
        }

        public int FisheriesUnitId { get; set; }
        public string? FullName { get; set; }
        public string? ShortName { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<FisheriesInventoryDetail> FisheriesInventoryDetails { get; set; }
        public virtual ICollection<ShopInventoryDetail> ShopInventoryDetails { get; set; }
        public virtual ICollection<ShopGoodSaleDetail> ShopGoodSaleDetails { get; set; }
        
    }
}
