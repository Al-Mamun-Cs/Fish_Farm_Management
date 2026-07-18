using MediatR;
using SchoolManagement.Application.DTOs.SupplierTypes;

namespace SchoolManagement.Application.Features.SupplierTypes.Requests.Queries
{
    public class GetSupplierTypeDetailRequest : IRequest<SupplierTypeDto>
    {
        public int SupplierTypeId { get; set; }
    }
}
