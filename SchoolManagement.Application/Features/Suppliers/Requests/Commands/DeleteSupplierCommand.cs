using MediatR;

namespace SchoolManagement.Application.Features.Suppliers.Requests.Commands
{
    public class DeleteSupplierCommand : IRequest
    {
        public int SupplierId { get; set; }
    }
}
