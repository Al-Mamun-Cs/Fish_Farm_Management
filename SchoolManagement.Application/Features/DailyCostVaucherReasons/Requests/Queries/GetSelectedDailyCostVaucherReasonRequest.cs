using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.DailyCostVaucherReasons.Requests.Queries
{
    public class GetSelectedDailyCostVaucherReasonRequest : IRequest<List<SelectedModel>>
    {
        public int WarehouseId { get; set; }
    }
}
