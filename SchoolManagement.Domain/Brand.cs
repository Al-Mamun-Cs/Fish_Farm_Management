using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public partial class Brand : BaseDomainEntity
    {
        public Brand()
        {
            //ProductTypes = new HashSet<ProductType>();
            
        }

        public int BrandId { get; set; }
        public string? FullName { get; set; }
        public string? ShortName { get; set; }
        public string? BrandImages { get; set; }
        public string? EshopImages { get; set; }
        public bool? IsEshop { get; set; }
        public bool IsActive { get; set; }

        //public virtual ICollection<ProductType> ProductTypes { get; set; }
        
    }
}
