using MediatR;

namespace SchoolManagement.Application.Features.SupplierTypes.Requests.Commands
{
    public class DeleteSupplierTypeCommand : IRequest
    {
        public int SupplierTypeId { get; set; }
    }
}
