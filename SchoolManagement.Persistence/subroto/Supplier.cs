using System;
using System.Collections.Generic;

namespace SchoolManagement.Persistence.subroto
{
    public partial class Supplier
    {
        public Supplier()
        {
            AdvancePayments = new HashSet<AdvancePayment>();
            BankTransactions = new HashSet<BankTransaction>();
            DuePaids = new HashSet<DuePaid>();
            GoodSalePayDueLogs = new HashSet<GoodSalePayDueLog>();
            GoodSales = new HashSet<GoodSale>();
            Inventories = new HashSet<Inventory>();
            PayablePaids = new HashSet<PayablePaid>();
        }

        public int SupplierId { get; set; }
        public int? CategoryId { get; set; }
        public int? ProductTypeId { get; set; }
        public int? WarehouseId { get; set; }
        public string? SupplierName { get; set; }
        public string? ShopName { get; set; }
        public string? Address { get; set; }
        public string? Tin { get; set; }
        public string? TradeLicenseNo { get; set; }
        public string? PhoneNo { get; set; }
        public string? Email { get; set; }
        public string? ContactPerson { get; set; }
        public decimal? TotalAdvanceAmount { get; set; }
        public decimal? TotalDueAmount { get; set; }
        public decimal? TotalPaidAmount { get; set; }
        public int? SupplierStatus { get; set; }
        public string? Remarks { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual EasyBikeType? Category { get; set; }
        public virtual ProductType? ProductType { get; set; }
        public virtual Warehouse? Warehouse { get; set; }
        public virtual ICollection<AdvancePayment> AdvancePayments { get; set; }
        public virtual ICollection<BankTransaction> BankTransactions { get; set; }
        public virtual ICollection<DuePaid> DuePaids { get; set; }
        public virtual ICollection<GoodSalePayDueLog> GoodSalePayDueLogs { get; set; }
        public virtual ICollection<GoodSale> GoodSales { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<PayablePaid> PayablePaids { get; set; }
    }
}
