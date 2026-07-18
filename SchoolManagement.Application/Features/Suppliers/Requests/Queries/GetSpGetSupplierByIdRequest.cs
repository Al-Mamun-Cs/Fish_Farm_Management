using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.Suppliers.Requests.Queries
{
    public class GetSpGetSupplierByIdRequest : IRequest<object>
    {
        public int? SupplierId { get; set; }
    }
}
  