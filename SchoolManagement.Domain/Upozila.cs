using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Domain
{
    public partial class Upozila : BaseDomainEntity
    {
        public Upozila()
        {
            Suppliers = new HashSet<Supplier>();
            
        }
        [Key]
        public int UpazilaId { get; set; }
        public int DistrictId { get; set; }
        public string UpazilaName { get; set; }
        public string? UpazilaNameBangla { get; set; }
        public int? Status { get; set; }
        public int? ManuPositon { get; set; }
        public bool IsActive { get; set; }

        public virtual District? District { get; set; }

        public virtual ICollection<Supplier> Suppliers { get; set; }

    }
}
