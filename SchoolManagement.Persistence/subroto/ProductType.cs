using System;
using System.Collections.Generic;

namespace SchoolManagement.Persistence.subroto
{
    public partial class ProductType
    {
        public ProductType()
        {
            GoodSales = new HashSet<GoodSale>();
            Inventories = new HashSet<Inventory>();
            ProductionLogs = new HashSet<ProductionLog>();
            Suppliers = new HashSet<Supplier>();
        }

        public int ProductTypeId { get; set; }
        public int? CategoryId { get; set; }
        public string? TypeName { get; set; }
        public int? FeesAmount { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual EasyBikeType? Category { get; set; }
        public virtual ICollection<GoodSale> GoodSales { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<ProductionLog> ProductionLogs { get; set; }
        public virtual ICollection<Supplier> Suppliers { get; set; }
    }
}
