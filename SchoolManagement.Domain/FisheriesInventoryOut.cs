using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public partial class FisheriesInventoryOut : BaseDomainEntity
    {
        public FisheriesInventoryOut()
        {
            //ProductTypes = new HashSet<ProductType>();
            
        }

        public int FisheriesInventoryOutId { get; set; }
        public int? WarehouseId { get; set; }
        public int? PondId { get; set; }
        public int? FisheriesProductTypeId { get; set; }
        public int? FisheriesInventoryDetailId { get; set; }
        public DateTime? Date { get; set; }
        public decimal? UseQty { get; set; }
        public decimal? UnitPurchasePrice { get; set; }
        public bool? ApproveStatus { get; set; }
        public bool IsActive { get; set; }

        public virtual Warehouse? Warehouse { get; set; }
        public virtual Pond? Pond { get; set; }
        public virtual FisheriesProductType? FisheriesProductType { get; set; }
        public virtual FisheriesInventoryDetail? FisheriesInventoryDetail { get; set; }

        //public virtual ICollection<ProductType> ProductTypes { get; set; }

    }
}
