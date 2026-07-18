using Microsoft.AspNetCore.Http;

namespace SchoolManagement.Application.DTOs.Suppliers
{
    public class CreateClientsDto
    {
        

        public IFormFile Photo { get; set; }
        public CreateSupplierDto SupplierForm { get; set; }
}
}
