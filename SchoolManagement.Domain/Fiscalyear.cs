using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public partial class Fiscalyear : BaseDomainEntity
    {
        public Fiscalyear()
        {
            //GlAccountTransactions = new HashSet<GlAccountTransaction>();
            
        }

        public int FiscalyearId { get; set; }
        public string? Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }

        //public virtual ICollection<GlAccountTransaction> GlAccountTransactions { get; set; }
        
    }
}
