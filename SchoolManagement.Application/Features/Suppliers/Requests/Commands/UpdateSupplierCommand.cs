using MediatR;
using SchoolManagement.Application.DTOs.Suppliers;

namespace SchoolManagement.Application.Features.Suppliers.Requests.Commands
{
    public class UpdateSupplierCommand : IRequest<Unit>
    {
        public CreateSupplierDto CreateSupplierDto { get; set; }
    }
}
