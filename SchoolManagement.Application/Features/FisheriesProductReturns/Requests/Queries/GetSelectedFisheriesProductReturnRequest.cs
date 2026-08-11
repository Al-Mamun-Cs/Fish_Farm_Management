using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.FisheriesProductReturns.Requests.Queries
{
    public class GetSelectedFisheriesProductReturnRequest : IRequest<List<SelectedModel>>
    {
        public int WarehouseId { get; set; }
    }
}
