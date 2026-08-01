using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Depositors.Requests.Queries
{
    public class GetSelectedDepositorRequest : IRequest<List<SelectedModel>>
    {
        public int WarehouseId { get; set; }
    }
}
