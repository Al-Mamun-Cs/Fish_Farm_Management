using System;
using System.Collections.Generic;

namespace SchoolManagement.Persistence.subroto
{
    public partial class PayablePaid
    {
        public int PayablePaidId { get; set; }
        public int? WarehouseId { get; set; }
        public int? SupplierId { get; set; }
        public int? PaymentStatusId { get; set; }
        public int? BankInfoId { get; set; }
        public string? ChequeNo { get; set; }
        public decimal? DuePaidAmount { get; set; }
        public DateTime? Date { get; set; }
        public string? Remarks { get; set; }
        public int? ApproveStatus { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? DateCreated { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool? IsActive { get; set; }

        public virtual EasyBikeBank? BankInfo { get; set; }
        public virtual PaymentStatus? PaymentStatus { get; set; }
        public virtual Supplier? Supplier { get; set; }
        public virtual Warehouse? Warehouse { get; set; }
    }
}
