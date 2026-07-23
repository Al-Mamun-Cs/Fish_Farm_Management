using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.ShopHandCashWithdrows
{
    public interface IShopHandCashWithdrowDto
    {
        public int ShopHandCashWithdrowId { get; set; }
        public int? WarehouseId { get; set; }
        public decimal? PresentInvestmentAmount { get; set; }
        public decimal? RemainingInvestmentAmount { get; set; }
        public decimal? PresentAmount { get; set; }
        public decimal? TransferAmount { get; set; }
        public decimal? RemainingAmount { get; set; }
        public DateTime? TransferDate { get; set; }
        public string? TransferReason { get; set; }
        public int? Type { get; set; }
        public int? ApproveStatus { get; set; }
        public string? ApproveBy { get; set; }
        public DateTime? ApproveDate { get; set; }
        public bool IsActive { get; set; }
    } 
}
