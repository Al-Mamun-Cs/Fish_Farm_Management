using System;
using System.Collections.Generic;

namespace SchoolManagement.Persistence.subroto
{
    public partial class EasyBikeBank
    {
        public EasyBikeBank()
        {
            BankTransactions = new HashSet<BankTransaction>();
            CashInHandLogs = new HashSet<CashInHandLog>();
            DuePaids = new HashSet<DuePaid>();
            Inventories = new HashSet<Inventory>();
            PayablePaids = new HashSet<PayablePaid>();
        }

        public int EasyBikeBankId { get; set; }
        public string? BankName { get; set; }
        public string? BankAccountNo { get; set; }
        public string? Address { get; set; }
        public string? PhoneNo { get; set; }
        public string? Email { get; set; }
        public decimal? BankBalance { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<BankTransaction> BankTransactions { get; set; }
        public virtual ICollection<CashInHandLog> CashInHandLogs { get; set; }
        public virtual ICollection<DuePaid> DuePaids { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<PayablePaid> PayablePaids { get; set; }
    }
}
