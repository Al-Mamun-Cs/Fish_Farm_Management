using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.FishSales.Requests.Queries
{
    public class GetSelectedFishSaleRequest : IRequest<List<SelectedModel>>
    {
        public int WarehouseId { get; set; }
    }
}
