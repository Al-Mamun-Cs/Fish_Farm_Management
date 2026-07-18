using System;
using System.Collections.Generic;

namespace SchoolManagement.Persistence.subroto
{
    public partial class Inventory
    {
        public Inventory()
        {
            BankTransactions = new HashSet<BankTransaction>();
            DuePaids = new HashSet<DuePaid>();
            GoodSales = new HashSet<GoodSale>();
            ProductionLogs = new HashSet<ProductionLog>();
        }

        public int InventoryId { get; set; }
        public int? WarehouseId { get; set; }
        public int? SupplierId { get; set; }
        public int? ProductTypeId { get; set; }
        public int? CategoryId { get; set; }
        public string? VoucherNo { get; set; }
        public int? MoneyReceived { get; set; }
        public int? InventoryType { get; set; }
        public int? BankInfoId { get; set; }
        public int? PaymentStatusId { get; set; }
        public decimal? PurchaseQty { get; set; }
        public decimal? AvailableQty { get; set; }
        public decimal? ProductionQty { get; set; }
        public int? ProductionCategoryId { get; set; }
        public int? ProductionProductTypeId { get; set; }
        public int? TotalBags { get; set; }
        public decimal? WeightingScaleNo { get; set; }
        public decimal? SellQty { get; set; }
        public decimal? DamageQty { get; set; }
        public decimal? ReturnQty { get; set; }
        public decimal? PurchasePrice { get; set; }
        public decimal? LessAmount { get; set; }
        public decimal? TotalPurchasePrice { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? DueAmount { get; set; }
        public decimal? DuePaidAmount { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public string? ChequeNo { get; set; }
        public decimal? TolyRent { get; set; }
        public string? Remarks { get; set; }
        public int? ApproveStatus { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual EasyBikeBank? BankInfo { get; set; }
        public virtual EasyBikeType? Category { get; set; }
        public virtual PaymentStatus? PaymentStatus { get; set; }
        public virtual ProductType? ProductType { get; set; }
        public virtual Supplier? Supplier { get; set; }
        public virtual Warehouse? Warehouse { get; set; }
        public virtual ICollection<BankTransaction> BankTransactions { get; set; }
        public virtual ICollection<DuePaid> DuePaids { get; set; }
        public virtual ICollection<GoodSale> GoodSales { get; set; }
        public virtual ICollection<ProductionLog> ProductionLogs { get; set; }
    }
}
