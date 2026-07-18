using System;
using System.Collections.Generic;

namespace SchoolManagement.Persistence.subroto
{
    public partial class BankTransaction
    {
        public int BankTransactionId { get; set; }
        public int? BankTransactionTypeId { get; set; }
        public int? BankInfoId { get; set; }
        public int? PaymentStatusId { get; set; }
        public int? SupplierId { get; set; }
        public int? InventoryId { get; set; }
        public int? WarehouseId { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string? Remarks { get; set; }
        public string? ChequeNo { get; set; }
        public DateTime? ChequeDate { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual EasyBikeBank? BankInfo { get; set; }
        public virtual BankTransactionType? BankTransactionType { get; set; }
        public virtual Inventory? Inventory { get; set; }
        public virtual PaymentStatus? PaymentStatus { get; set; }
        public virtual Supplier? Supplier { get; set; }
        public virtual Warehouse? Warehouse { get; set; }
    }
}
