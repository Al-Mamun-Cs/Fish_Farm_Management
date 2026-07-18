using System;
using System.Collections.Generic;

namespace SchoolManagement.Persistence.subroto
{
    public partial class GoodSale
    {
        public GoodSale()
        {
            GoodSaleDetails = new HashSet<GoodSaleDetail>();
            GoodSalePayDueLogs = new HashSet<GoodSalePayDueLog>();
        }

        public int GoodSaleId { get; set; }
        public int? InventoryId { get; set; }
        public int? SupplierId { get; set; }
        public int? PaymentStatusId { get; set; }
        public int? BankInfoId { get; set; }
        public int? ProductTypeId { get; set; }
        public int? CategoryId { get; set; }
        public int? MoneyReceived { get; set; }
        public decimal? SaleQty { get; set; }
        public decimal? SalePrice { get; set; }
        public decimal? SaleDueAmount { get; set; }
        public decimal? SaleLessAmount { get; set; }
        public decimal? TotalSalePrice { get; set; }
        public decimal? SalePaidAmount { get; set; }
        public decimal? SaleDuePaidAmount { get; set; }
        public decimal? WeightingScaleNo { get; set; }
        public string? VoucherNo { get; set; }
        public string? ChequeNo { get; set; }
        public DateTime? SaleDate { get; set; }
        public decimal? TolyRent { get; set; }
        public string? Remarks { get; set; }
        public int? ApproveStatus { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? DateCreated { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool? IsActive { get; set; }

        public virtual EasyBikeType? Category { get; set; }
        public virtual Inventory? Inventory { get; set; }
        public virtual PaymentStatus? PaymentStatus { get; set; }
        public virtual ProductType? ProductType { get; set; }
        public virtual Supplier? Supplier { get; set; }
        public virtual ICollection<GoodSaleDetail> GoodSaleDetails { get; set; }
        public virtual ICollection<GoodSalePayDueLog> GoodSalePayDueLogs { get; set; }
    }
}
