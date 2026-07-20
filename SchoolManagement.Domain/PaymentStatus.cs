using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public partial class PaymentStatus : BaseDomainEntity
    {
        public PaymentStatus()
        {
            FisheriesInventorys = new HashSet<FisheriesInventory>();
            ShopInventorys = new HashSet<ShopInventory>();
            DailyMiscellaneousCosts = new HashSet<DailyMiscellaneousCost>();
            ShopGoodSales = new HashSet<ShopGoodSale>();
        }

        public int PaymentStatusId { get; set; }
        public string? StatusName { get; set; }
        public int? PriorityNo { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<FisheriesInventory> FisheriesInventorys { get; set; }
        public virtual ICollection<ShopInventory> ShopInventorys { get; set; }
        public virtual ICollection<DailyMiscellaneousCost> DailyMiscellaneousCosts { get; set; }
        public virtual ICollection<ShopGoodSale> ShopGoodSales { get; set; }
    }
}
