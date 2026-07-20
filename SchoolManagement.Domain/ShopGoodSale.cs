using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public partial class ShopGoodSale : BaseDomainEntity
    {
        public ShopGoodSale()
        {
            ShopGoodSaleDetails = new HashSet<ShopGoodSaleDetail>();
            
        }

        public int ShopGoodSaleId { get; set; }
        public int? WarehouseId { get; set; }
        public int? SupplierId { get; set; }
        public int? PaymentStatusId { get; set; }
        public string? VoucherNo { get; set; }
        public DateTime? SaleDate { get; set; }
        public decimal? TotalSalePrice { get; set; }
        public decimal? SaleLessAmount { get; set; }
        public decimal? GrandTotalSalePrice { get; set; }
        public decimal? CustomerPaidAmount { get; set; }
        public decimal? CustomerDueAmount { get; set; }
        public string? Remarks { get; set; }
        public int? ApproveStatus { get; set; }
        public bool IsActive { get; set; }

        public virtual Warehouse? Warehouse { get; set; }
        public virtual Supplier? Supplier { get; set; }
        public virtual PaymentStatus? PaymentStatus { get; set; }

        public virtual ICollection<ShopGoodSaleDetail> ShopGoodSaleDetails { get; set; }

    }
}
