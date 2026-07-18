using MediatR;
using SchoolManagement.Application.DTOs.Suppliers;

namespace SchoolManagement.Application.Features.Suppliers.Requests.Queries
{
    public class GetSupplierDetailRequest : IRequest<SupplierDto>
    {
        public int SupplierId { get; set; }
    }
}
