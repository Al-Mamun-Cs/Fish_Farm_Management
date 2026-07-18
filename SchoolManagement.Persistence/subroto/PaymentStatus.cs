using System;
using System.Collections.Generic;

namespace SchoolManagement.Persistence.subroto
{
    public partial class PaymentStatus
    {
        public PaymentStatus()
        {
            AdvancePayments = new HashSet<AdvancePayment>();
            BankTransactions = new HashSet<BankTransaction>();
            CashInHandLogs = new HashSet<CashInHandLog>();
            CashInHands = new HashSet<CashInHand>();
            DuePaids = new HashSet<DuePaid>();
            GoodSales = new HashSet<GoodSale>();
            Inventories = new HashSet<Inventory>();
            PayablePaids = new HashSet<PayablePaid>();
        }

        public int PaymentStatusId { get; set; }
        public string? StatusName { get; set; }
        public int? PriorityNo { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<AdvancePayment> AdvancePayments { get; set; }
        public virtual ICollection<BankTransaction> BankTransactions { get; set; }
        public virtual ICollection<CashInHandLog> CashInHandLogs { get; set; }
        public virtual ICollection<CashInHand> CashInHands { get; set; }
        public virtual ICollection<DuePaid> DuePaids { get; set; }
        public virtual ICollection<GoodSale> GoodSales { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<PayablePaid> PayablePaids { get; set; }
    }
}
