using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public partial class EasyBikeBank : BaseDomainEntity
    {
        public EasyBikeBank()
        {
            DailyMiscellaneousCosts = new HashSet<DailyMiscellaneousCost>();
        }

        public int EasyBikeBankId { get; set; }
        public int? AccountNameId { get; set; }
        public int? AccountTypeId { get; set; }
        public string? BankName { get; set; }
        public string? BankAccountNo { get; set; }
        public string? Address { get; set; }
        public string? PhoneNo { get; set; }
        public decimal? BankBalance { get; set; }
        public string? Email { get; set; }
        public bool IsActive { get; set; }


        public virtual ICollection<DailyMiscellaneousCost> DailyMiscellaneousCosts { get; set; }

    }
}
