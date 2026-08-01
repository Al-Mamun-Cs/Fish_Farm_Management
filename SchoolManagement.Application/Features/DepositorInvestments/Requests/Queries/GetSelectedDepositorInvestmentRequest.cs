using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.DepositorInvestments.Requests.Queries
{
    public class GetSelectedDepositorInvestmentRequest : IRequest<List<SelectedModel>>
    {
        public int WarehouseId { get; set; }
    }
}
