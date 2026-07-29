using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public partial class FishSale : BaseDomainEntity
    {
        public FishSale()
        {
            //Empolyees = new HashSet<Empolyee>();

        }

        public int FishSaleId { get; set; }
        public int? WarehouseId { get; set; }
        public int? PondId { get; set; }
        public int? SupplierId { get; set; }
        public int? FisheriesUnitId { get; set; }
        public int? PaymentStatusId { get; set; }
        public DateTime? SaleDate { get; set; }
        public decimal? SaleQty { get; set; }
        public decimal? UnitSalePrice { get; set; }
        public decimal? TotalSalePrice { get; set; }
        public decimal? SalePaidAmount { get; set; }
        public decimal? SaleDueAmount { get; set; }
        public bool IsActive { get; set; }

        public virtual Warehouse? Warehouse { get; set; }
        public virtual Pond? Pond { get; set; }
        public virtual Supplier? Supplier { get; set; }
        public virtual FisheriesUnit? FisheriesUnit { get; set; }
        public virtual PaymentStatus? PaymentStatus { get; set; }

        //public virtual ICollection<Empolyee> Empolyees { get; set; }

    }
}
