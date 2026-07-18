using MediatR;
using SchoolManagement.Application.DTOs.SupplierTypes;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.SupplierTypes.Requests.Commands
{
    public class CreateSupplierTypeCommand : IRequest<BaseCommandResponse>
    {
        public CreateSupplierTypeDto SupplierTypeDto { get; set; }
    }
}
