using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public partial class Warehouse : BaseDomainEntity
    {
        public Warehouse()
        {
            FisheriesInventorys = new HashSet<FisheriesInventory>();
            FisheriesInventoryOuts = new HashSet<FisheriesInventoryOut>();
        }

        public int WarehouseId { get; set; }
        public string? WarehouseName { get; set; }
        public string? WarehouseAddress { get; set; }
        public string? Location { get; set; }
        public string? ContactPerson { get; set; }
        public string? ContactNo { get; set; }
        public decimal? CashAmount { get; set; }
        public string? Remarks { get; set; }
        public string? ProductImages { get; set; }
        public string? BusinessImages { get; set; }
        public string? BusinessUnitName { get; set; }
        public string? BussinessUnitDescriptions { get; set; }
        public bool? IsWebsite { get; set; }
        public bool? IsEshop { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Supplier> Suppliers { get; set; }
        public virtual ICollection<Designation> Designations { get; set; }
        public virtual ICollection<FisheriesInventory> FisheriesInventorys { get; set; }
        public virtual ICollection<FisheriesInventoryOut> FisheriesInventoryOuts { get; set; }
    }
}
