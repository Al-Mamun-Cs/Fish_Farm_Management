using MediatR;
using SchoolManagement.Application.DTOs.SupplierTypes;

namespace SchoolManagement.Application.Features.SupplierTypes.Requests.Commands
{
    public class UpdateSupplierTypeCommand : IRequest<Unit>
    {
        public SupplierTypeDto SupplierTypeDto { get; set; }
    }
}
