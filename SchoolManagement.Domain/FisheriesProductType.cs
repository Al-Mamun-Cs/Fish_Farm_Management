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
            ShopInventoryDetails = new HashSet<ShopInventoryDetail>();
            ShopGoodSaleDetails = new HashSet<ShopGoodSaleDetail>();
            
        }

        public int FisheriesProductTypeId { get; set; }
        public int? WarehouseId { get; set; }
        public string? NameEnglish { get; set; }
        public string? NameBangla { get; set; }
        public bool IsActive { get; set; }
        public virtual Warehouse? Warehouse { get; set; }


        public virtual ICollection<FisheriesInventoryDetail> FisheriesInventoryDetails { get; set; }
        public virtual ICollection<ShopInventoryDetail> ShopInventoryDetails { get; set; }
        public virtual ICollection<FisheriesInventoryOut> FisheriesInventoryOuts { get; set; }
        public virtual ICollection<ShopGoodSaleDetail> ShopGoodSaleDetails { get; set; }
        
    }
}
