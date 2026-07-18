using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public partial class Bank : BaseDomainEntity
    {
        public Bank()
        {
            //Empolyees = new HashSet<Empolyee>();

        }

        public int BankId { get; set; }
        public string? BankName { get; set; }
        public bool IsActive { get; set; }

        //public virtual ICollection<Empolyee> Empolyees { get; set; }

    }
}
