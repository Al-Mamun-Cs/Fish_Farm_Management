using System;
using System.Collections.Generic;

namespace SchoolManagement.Persistence.subroto
{
    public partial class Warehouse
    {
        public Warehouse()
        {
            AdvancePayments = new HashSet<AdvancePayment>();
            BankTransactions = new HashSet<BankTransaction>();
            CashDebits = new HashSet<CashDebit>();
            CashInHandLogs = new HashSet<CashInHandLog>();
            CashInHands = new HashSet<CashInHand>();
            DuePaids = new HashSet<DuePaid>();
            Inventories = new HashSet<Inventory>();
            PayablePaids = new HashSet<PayablePaid>();
            Suppliers = new HashSet<Supplier>();
        }

        public int WarehouseId { get; set; }
        public string? WarehouseName { get; set; }
        public string? WarehouseAddress { get; set; }
        public string? Location { get; set; }
        public string? ContactPerson { get; set; }
        public string? ContactNo { get; set; }
        public decimal? CashAmount { get; set; }
        public string? Remarks { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<AdvancePayment> AdvancePayments { get; set; }
        public virtual ICollection<BankTransaction> BankTransactions { get; set; }
        public virtual ICollection<CashDebit> CashDebits { get; set; }
        public virtual ICollection<CashInHandLog> CashInHandLogs { get; set; }
        public virtual ICollection<CashInHand> CashInHands { get; set; }
        public virtual ICollection<DuePaid> DuePaids { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<PayablePaid> PayablePaids { get; set; }
        public virtual ICollection<Supplier> Suppliers { get; set; }
    }
}
