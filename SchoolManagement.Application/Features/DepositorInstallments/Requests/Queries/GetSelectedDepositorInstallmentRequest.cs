using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.DepositorInstallments.Requests.Queries
{
    public class GetSelectedDepositorInstallmentRequest : IRequest<List<SelectedModel>>
    {
        public int WarehouseId { get; set; }
    }
}
