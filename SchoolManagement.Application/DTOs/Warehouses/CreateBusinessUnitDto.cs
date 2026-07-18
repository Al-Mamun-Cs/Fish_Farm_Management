using Microsoft.AspNetCore.Http;

namespace SchoolManagement.Application.DTOs.Warehouses
{
    public class CreateBusinessUnitDto
    {
        

        public IFormFile Photo { get; set; }
        public IFormFile Image { get; set; }
        public CreateWarehouseDto WarehouseForm { get; set; }
}
}
