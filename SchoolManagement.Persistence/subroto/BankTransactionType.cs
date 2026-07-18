using System;
using System.Collections.Generic;

namespace SchoolManagement.Persistence.subroto
{
    public partial class BankTransactionType
    {
        public BankTransactionType()
        {
            BankTransactions = new HashSet<BankTransaction>();
        }

        public int BankTransactionTypeId { get; set; }
        public string? Name { get; set; }
        public int? Status { get; set; }
        public int? Position { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<BankTransaction> BankTransactions { get; set; }
    }
}
