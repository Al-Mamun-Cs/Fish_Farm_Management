using System;
using System.Collections.Generic;

namespace SchoolManagement.Persistence.subroto
{
    public partial class GoodSalePayDueLog
    {
        public int GoodSalePayDueLogId { get; set; }
        public int? GoodSaleId { get; set; }
        public int? SupplierId { get; set; }
        public decimal? SaleQty { get; set; }
        public decimal? SaleDueAmount { get; set; }
        public string? VoucherNo { get; set; }
        public DateTime? SaleDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? DateCreated { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool? IsActive { get; set; }

        public virtual GoodSale? GoodSale { get; set; }
        public virtual Supplier? Supplier { get; set; }
    }
}
