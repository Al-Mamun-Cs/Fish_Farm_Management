using System;
using System.Collections.Generic;

namespace SchoolManagement.Persistence.subroto
{
    public partial class GoodSaleDetail
    {
        public int GoodSaleDetailId { get; set; }
        public int? ProductTypeId { get; set; }
        public int? CategoryId { get; set; }
        public decimal? SaleQty { get; set; }
        public decimal? SalePrice { get; set; }
        public int GoodSaleId { get; set; }

        public virtual GoodSale GoodSale { get; set; } = null!;
    }
}
