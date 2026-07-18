using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.SupplierTypes
{
    public class CreateSupplierTypeDto : ISupplierTypeDto
    {
        public int SupplierTypeId { get; set; }
        public string? SupplierTypeName { get; set; }
        public string? Remarks { get; set; }
        public bool IsActive { get; set; }
    }
}
