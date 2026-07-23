using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.DailyCostVaucherReasons
{
    public interface IDailyCostVaucherReasonDto
    {
        public int DailyCostVaucherReasonId { get; set; }
        public int? WarehouseId { get; set; }
        public string? FullName { get; set; }
        public string? ShortName { get; set; }
        public int? MenuPosition { get; set; }
        public int? TransactionType { get; set; }
        public bool IsActive { get; set; }
    } 
}
