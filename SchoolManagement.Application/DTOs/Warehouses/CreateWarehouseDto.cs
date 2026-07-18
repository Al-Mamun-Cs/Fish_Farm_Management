using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.Warehouses
{
    public class CreateWarehouseDto : IWarehouseDto
    {
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

        public IFormFile? Photo { get; set; }
        public IFormFile? Image { get; set; }
    }
}
