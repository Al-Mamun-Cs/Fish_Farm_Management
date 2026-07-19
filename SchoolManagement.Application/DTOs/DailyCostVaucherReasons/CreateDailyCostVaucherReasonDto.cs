using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.DailyCostVaucherReasons
{
    public class CreateDailyCostVaucherReasonDto : IDailyCostVaucherReasonDto
    {
        public int DailyCostVaucherReasonId { get; set; }
        public int? WarehouseId { get; set; }
        public string? FullName { get; set; }
        public string? ShortName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
