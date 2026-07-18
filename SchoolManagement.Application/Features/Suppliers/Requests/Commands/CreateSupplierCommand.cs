using MediatR;
using SchoolManagement.Application.DTOs.Suppliers;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.Suppliers.Requests.Commands
{
    public class CreateSupplierCommand : IRequest<BaseCommandResponse>
    {
        public CreateSupplierDto SupplierDto { get; set; }
    }
}
