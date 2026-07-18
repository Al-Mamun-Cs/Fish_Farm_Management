using System;
using System.Collections.Generic;

namespace SchoolManagement.Persistence.subroto
{
    public partial class EasyBikeType
    {
        public EasyBikeType()
        {
            AdvancePayments = new HashSet<AdvancePayment>();
            GoodSales = new HashSet<GoodSale>();
            Inventories = new HashSet<Inventory>();
            ProductTypes = new HashSet<ProductType>();
            ProductionLogs = new HashSet<ProductionLog>();
            Suppliers = new HashSet<Supplier>();
        }

        public int EasyBikeTypeId { get; set; }
        public string? TypeName { get; set; }
        public int? FeesAmount { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<AdvancePayment> AdvancePayments { get; set; }
        public virtual ICollection<GoodSale> GoodSales { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<ProductType> ProductTypes { get; set; }
        public virtual ICollection<ProductionLog> ProductionLogs { get; set; }
        public virtual ICollection<Supplier> Suppliers { get; set; }
    }
}
